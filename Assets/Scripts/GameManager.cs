using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

// create enum for starting game state
    public enum StartGameState
    {
        Start,
        Restart,
        Next,
        GameOver
    }

    public int maxRounds = 10;      // Maximum number of rounds
    public int itemCount = 4;       // Number of items to display in each round
    public float roundDuration = 12; // Duration of each round in seconds
    public Text scoreText;          // UI Text element for displaying the score
    public Text roundText;          // UI Text element for displaying the round number
    public Text timerText;          // UI Text element for displaying the round timer
    public ItemGenerator itemGenerator;    // Reference to the ItemGenerator script
    public SceneController sceneController;  // Reference to the SceneController script
    public FuzzyScreen fuzzyScreen;          // Reference to the FuzzyScreen script
    public int sceneNum = 1;            // The current scene number
    public StartGameState startGameState = StartGameState.Start; // Current start game state
    public int score = 0;          // Current player score
    public GameObject statusBar;   // Reference to the status bar game object

    private int round = 0;          // Current round number
    private bool gameRunning = false;  // Flag to indicate if the game is currently running

    // Start the game
    public void StartGame()
    {
        statusBar.SetActive(true);
        gameRunning = true;
        UpdateScoreUI();
        UpdateRoundUI();
        StartRound();
    }

    // End the game
    public void EndGame()
    {
        gameRunning = false;

        // Disable the SceneController to prevent any more scenes from being loaded
        FindObjectOfType<SceneController>().enabled = false;

        // // Get a reference to the UI Manager
        // UIManager uiManager = FindObjectOfType<UIManager>();

        // // Set the game over text and display the game over panel
        // uiManager.SetGameOverText();
        // uiManager.ShowGameOverPanel();

        // Clean up any other game objects or data as needed
    }


    // Start a new round
    private void StartRound()
    {

        // Check start game state
        switch (startGameState)
        {
            case StartGameState.Start:
                round = 1;
                break;
            case StartGameState.Restart:
                break;
            case StartGameState.Next:
                round++;
                break;
            case StartGameState.GameOver:
                EndGame();
                break;
        }
        
        UpdateRoundUI();

        // Hide the start panel
        sceneController.HideStartPanel();

        // Start game from the SceneController
        sceneController.StartGame(round);


        // // Generate new set of items
        // itemGenerator.GenerateItems(itemCount);

        // // Display the items on screen
        // sceneController.DisplayItems();

        // Start timer for round duration
        StartCoroutine(RoundTimer());
        
    }

    // Pause the game
    public void PauseGame()
    {
        gameRunning = false;
        sceneController.ShowStartPanel();

        // Stop the timer
        StopAllCoroutines();

    }

    private IEnumerator RoundTimer()
    {
        float remainingTime = roundDuration;
        sceneNum = 1;
        while (remainingTime > 0)
        {
            yield return null;
            remainingTime -= Time.deltaTime;
            UpdateTimerUI(remainingTime);
            if(roundDuration-sceneController.itemDisplayTime+fuzzyScreen.duration<=remainingTime && sceneNum==1)
            {
                sceneNum = 2;
            }
        }
        EndRound();
    }

    // Update the timer UI element
    private void UpdateTimerUI(float remainingTime)
    {
        timerText.text = "Time: " + remainingTime.ToString("F2");
    }
    
    // End the current round and transition to the next one
    public void EndRound()
    {
        // Clear the items from the screen
        sceneController.ClearItems();

        // Check if the player successfully identified the added item
        bool success = sceneController.GetIsCorrectItem();

        // If so, update score
        if (success)
        {
            score++;
            UpdateScoreUI();
        }

        // Check if the maximum number of rounds has been reached
        if (round >= maxRounds)
        {
            EndGame();
        }
        else
        {
            // If not, run the TimeUp function in the SceneController
            sceneController.TimeUp();

            // StartRound();
        }
    }


    // increment the score
    public void IncrementScore()
    {
        score++;
        UpdateScoreUI();
    }

    // Restart the score
    public void RestartScore()
    {
        score = 0;
        UpdateScoreUI();
    }
    // Update the score UI element
    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    // Update the round UI element
    private void UpdateRoundUI()
    {
        roundText.text = "Round " + round.ToString() + " of " + maxRounds.ToString();
    }

    // Return the state of the game
    public bool GetGameRunning()
    {
        return gameRunning;
    }
}
