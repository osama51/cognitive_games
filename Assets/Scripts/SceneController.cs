using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public Text roundText;              // Text object to display the current round number
    public Text feedbackText;           // Text object to display feedback to the player
    public Text feedbackTextHiero;      // Text object to display feedback to the player in hieroglyphics

    public GameObject startButton;      // Start button to begin the game
    public Text startButtonText;        // Text object to display the start button text
    public Text startButtonTextHiero;   // Text object to display the start button text in hieroglyphics

    public GameObject[] itemSlots;      // Array of item slots to display the items
    public float itemDisplayTime = 5.0f;       // Time to display the items in seconds
    public GameObject startPanel;       // Panel to display the start button
    public GameObject feedBackPanel;    // Panel to display feedback to the player
    public GameManager gameManager;     // Reference to the GameManager script
    public ItemGenerator itemGenerator; // Reference to the ItemGenerator script
    public DifficultyManager difficultyManager; // Reference to the DifficultyManager script
    public FuzzyScreen fuzzyScreen;     // Reference to the FuzzyScreen script
    public GameObject parentObject;     // Reference to the parent object of the items
    public GameObject panelToCoverListeners; // Panel to cover the listeners

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
        Invoke("HideItems", itemDisplayTime);
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
    private void HideItems()
    {
        // if game is running (not paused)
        if(gameManager.GetGameRunning())
        {
            // Disable the item that was added in the first scene
            // items[addedItemIndex].SetActive(false);

            // // Display each item in a random slot
            // for (int i = 0; i < items.Length; i++)
            // {
            //     // int randomSlot = Random.Range(0, itemSlots.Length);
            //     // items[i].transform.position = itemSlots[randomSlot].transform.position;
            //     items[i].SetActive(false);
            // }

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
        }
        else
        {
            gameManager.startGameState = GameManager.StartGameState.Restart;
            isCorrectItem = false;
            feedbackText.text = "Incorrect. Try again.";
            feedbackTextHiero.text = feedbackText.text;
            // Enable the start button to restart the round
            startButtonText.text = "Restart Round";
            startButtonTextHiero.text = startButtonText.text ;
            startButton.SetActive(true);
        }

        // Clear the items from the previous round
        ClearItems();
    }

    // time's up
    public void TimeUp()
    {
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
    }

    public bool GetIsCorrectItem()
    {
        return isCorrectItem;
    }
}
