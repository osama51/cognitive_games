using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public Text gameOverText;           // Text object to display the game over message
    public Text roundText;              // Text object to display the current round number
    public Text feedbackText;           // Text object to display feedback to the player
    public Text feedbackTextHiero;      // Text object to display feedback to the player in hieroglyphics

    public Text groupText;              // Text object to display our group name
    public GameObject startButton;      // Start button to begin the game
    public Text startButtonText;        // Text object to display the start button text
    public Text startButtonTextHiero;   // Text object to display the start button text in hieroglyphics

    public GameObject backButton;      // Back button to return to the main menu       

    public GameObject[] itemSlots;      // Array of item slots to display the items
    public float itemDisplayTime = 5.0f;       // Time to display the items in seconds
    public GameObject startPanel;       // Panel to display the start button
    public GameObject feedBackPanel;    // Panel to display feedback to the player
    public GameObject summaryPanel;     // Panel to display the summary of the game
    public Text finalScoreText;         // Text object to display the final score
    public Text finalRoundText;         // Text object to display the final round number
    public Text finalTimeText;          // Text object to display the final time

    public GameObject titlePanel;      // Panel to display the title of the game

    public GameManager gameManager;     // Reference to the GameManager script
    public ItemGenerator itemGenerator; // Reference to the ItemGenerator script
    public DifficultyManager difficultyManager; // Reference to the DifficultyManager script
    public FuzzyScreen fuzzyScreen;     // Reference to the FuzzyScreen script
    public GameObject parentObject;     // Reference to the parent object of the items
    public GameObject panelToCoverListeners; // Panel to cover the listeners
    public Coroutine hideItemsCoroutine;      // Id of the invoke method to hide the items

    private int roundNumber;            // Current round number
    private GameObject[] items;         // Array of items generated for the current round
    private bool[] occupiedSlots;     // Array of flags to indicate if a slot is occupied
    private int addedItemIndex;         // Index of the item that will be added in the second scene
    private bool isCorrectItem;         // Flag to indicate if the player selected the correct item
    private Vector3 position;          // Position of the item

    // Start the game
    public void StartGame(int roundNumber)
    {
        roundText.text = "Round: " + roundNumber;
        feedbackText.text = "";
        itemGenerator.GenerateItems(difficultyManager.GetItemCount());
        items = itemGenerator.GetItems();
        addedItemIndex = itemGenerator.GetAddedItemIndex();
        DisplayItems();
    }

    // Display the items for the current round
    public void DisplayItems()
    {
        if(gameManager.sceneNum==1){
            DisplayFirstScene();
        } else if(gameManager.sceneNum==2){
            DisplaySecondScene();
        }
        
    }

    // Display the first scene
    public void DisplayFirstScene()
    {
        // Click listeners are disabled
        panelToCoverListeners.SetActive(true);

        // print the added item index
        print("addedItemIndex: " + addedItemIndex);

        // print the count of items
        print("items.Length: " + items.Length);

        occupiedSlots = new bool[itemSlots.Length];
        for (int i = 0; i < occupiedSlots.Length; i++)
        {
            occupiedSlots[i] = false;
        }

        // Display each item in a random slot
        for (int i = 0; i < items.Length; i++)
        {
            // Find an unoccupied slot
            int randomSlot;
            do
            {
                randomSlot = Random.Range(0, itemSlots.Length);
            } while (occupiedSlots[randomSlot]);

            // Set the slot as occupied
            occupiedSlots[randomSlot] = true;

            // int randomSlot = Random.Range(0, itemSlots.Length);

            // Vector3 slotPosition = itemSlots[randomSlot].transform.position;
            // Vector3 localSlotPosition = parentObject.transform.InverseTransformPoint(slotPosition);
            // items[i].transform.localPosition = localSlotPosition;

            items[i].transform.position = itemSlots[randomSlot].transform.position;
            // generate random position for the item

            // // make sure there's space between the item and all the previous items
            // items[i].transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-2f, 2f), 0f);

            // // make sure the item is not too close to the center and to each other
            // while (items[i].transform.position.x < 1f && items[i].transform.position.x > -1f)
            // {
            //     items[i].transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-2f, 2f), 0f);
            // }

            // check if the item is the added item
            if (i != addedItemIndex)
            {
                items[i].SetActive(true);
            }
            else
            {
                items[i].SetActive(false);
            }
            

            // draw a red border around the added item
            // items[i].GetComponent<SpriteRenderer>().color = Color.red;

            // bring the item to the front
            // items[i].GetComponent<SpriteRenderer>().sortingOrder = 1;
        }

        // Wait for the display time, then hide the items and display the second scene
        // hideItemsInvokeId = Invoke("HideItems", itemDisplayTime);
        hideItemsCoroutine = StartCoroutine(HideItemsCoroutine());
    }

    // Display the second scene
    public void DisplaySecondScene()
    {
        panelToCoverListeners.SetActive(false);
        // Find an unoccupied slot
        int randomSlot;
        do
        {
            randomSlot = Random.Range(0, itemSlots.Length);
        } while (occupiedSlots[randomSlot]);

        // Set the slot as occupied
        occupiedSlots[randomSlot] = true;

        items[addedItemIndex].transform.position = itemSlots[randomSlot].transform.position;
        items[addedItemIndex].SetActive(true);
    }

    // Hide the items and display the second scene with the added item
    // private void HideItems()
    // {
    private IEnumerator HideItemsCoroutine()
    {
        yield return new WaitForSeconds(itemDisplayTime);
        // if game is running (not paused)
        if(gameManager.GetGameRunning())
        {
            // Display the fuzzy screen
            fuzzyScreen.Show();
        }
    }

    // Clear the items from the previous round
    public void ClearItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            Destroy(items[i]);
        }
    }

    // Hide the start panel
    public void HideStartPanel()
    {
        startPanel.SetActive(false);
    }

    // Show the start panel
    public void ShowStartPanel()
    {
        startPanel.SetActive(true);
    }

    // Hide the feedback panel
    public void HideFeedbackPanel()
    {
        feedBackPanel.SetActive(false);
    }

    // Hide the start button
    public void HideStartButton()
    {
        startButton.SetActive(false);
    }

    // Hide the fuzzy screen
    public void HideFuzzyScreen()
    {
        fuzzyScreen.Hide();
    }

    // // Begin the next round
    // public void NextRound()
    // {
    //     roundNumber++;
    //     roundText.text = "Round " + roundNumber;
    //     feedbackText.text = "";
    //     itemGenerator.GenerateItems(gameManager.itemCount);
    //     items = itemGenerator.GetItems();
    //     addedItemIndex = itemGenerator.GetAddedItemIndex();
    //     DisplayItems();
    // }

    // Check if the player correctly identified the added item
    public void CheckAnswer(int selectedItemIndex)
    {
        gameManager.sceneNum=1;
        // Show the starting panel
        ShowStartPanel();
        // Hide the start button 
        HideStartButton();

        gameManager.PauseGame();
        
        // hide the groupText
        groupText.text = "";
        
        if (selectedItemIndex == addedItemIndex)
        {
            gameManager.startGameState = GameManager.StartGameState.Next;
            isCorrectItem = true;
            gameManager.IncrementScore();
            feedbackText.text = "Correct!";
            feedbackTextHiero.text = feedbackText.text;

            // Enable the start button to begin the next round
            startButtonText.text = "Next Round";
            startButtonTextHiero.text = startButtonText.text;
            startButton.SetActive(true);

            if(gameManager.round == 15)
            {
                gameOverText.text = "Well Done!";
                GameOver("Won");
            }
        }
        else
        {
            if(gameManager.score > 0)
            {
                gameManager.score = gameManager.score - 1;
                gameManager.UpdateScoreUI();
            
                gameManager.startGameState = GameManager.StartGameState.Restart;
                isCorrectItem = false;
                feedbackText.text = "Incorrect. Try again.";
                feedbackTextHiero.text = feedbackText.text;
                // Enable the start button to restart the round
                startButtonText.text = "Restart Round";
                startButtonTextHiero.text = startButtonText.text ;
                startButton.SetActive(true);
            } else {
                GameOver("Lost");
            }
        }

        // Clear the items from the previous round
        ClearItems();
    }

    // time's up
    public void TimeUp()
    {

        if(gameManager.score > 0)
        {
            gameManager.score = gameManager.score - 1;
            gameManager.UpdateScoreUI();
            gameManager.sceneNum=1;
            gameManager.startGameState = GameManager.StartGameState.Restart;
            // Pause and show the starting panel
            gameManager.PauseGame();
            // Hide the start button 
            HideStartButton();
            feedbackText.text = "Time's up. Try again.";
            feedbackTextHiero.text = feedbackText.text;
            // Enable the start button to restart the round
            startButtonText.text = "Restart Round";
            startButtonTextHiero.text = startButtonText.text ;
            startButton.SetActive(true);
        } else {
            GameOver("Lost");
        }
    }

    public void GameOver(string WonLost)
    {
        ClearItems();
        backButton.SetActive(false);
        gameManager.sceneNum=1;
        gameManager.startGameState = GameManager.StartGameState.GameOver;
        print("THE GAME IS OVER" + WonLost + " " + gameManager.startGameState);
        // Show the starting panel
        ShowStartPanel();
        // Hide the title panel
        HideTitlePanel();
        // Show the summary panel
        ShowSummaryPanel();
        
        if(WonLost == "Won")
        {
            gameOverText.text = "Well Done!";
        } else {
            gameOverText.text = "Game Over";
        }

        gameManager.statusBar.SetActive(false);
        finalRoundText.text = "Round " + gameManager.round;
        finalScoreText.text = "Highest Score: " + (gameManager.round - 1);
        finalTimeText.text = "Time: " + gameManager.totalTime.ToString("F2") + "s";

        // Pause and show the starting panel
        gameManager.PauseGame();
        // Enable the start button to restart the round
        startButtonText.text = "Main Menu";
        startButtonTextHiero.text = startButtonText.text ;
        startButton.SetActive(true);
    }

    public void EndGame(){

        print("END GAME IS  CALLED");
        backButton.SetActive(false);
        gameManager.sceneNum=1;
        gameManager.score = 0;
        difficultyManager.fuzzyScreenDuration = 1f;

        gameManager.PauseGame();

        ClearItems();
        
        gameManager.statusBar.SetActive(false);

        // hide the groupText
        groupText.text = "";

        ShowStartPanel();
        ShowTitlePanel();
        HideSummaryPanel();


        groupText.text = "SBME23 - GROUP 12";
        feedbackText.text = "Find The Intrusive Scarab";
        feedbackTextHiero.text = feedbackText.text;

        startButtonText.text = "Start";
        startButtonTextHiero.text = startButtonText.text ;

        gameManager.startGameState = GameManager.StartGameState.Start;
    }

    private void HideTitlePanel()
    {
        titlePanel.SetActive(false);
    }

    private void ShowTitlePanel()
    {
        titlePanel.SetActive(true);
    }

    private void ShowSummaryPanel()
    {
        summaryPanel.SetActive(true);
    }

    private void HideSummaryPanel()
    {
        summaryPanel.SetActive(false);
    }




    public bool GetIsCorrectItem()
    {
        return isCorrectItem;
    }
}
