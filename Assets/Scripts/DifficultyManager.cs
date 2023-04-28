using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    // The initial number of items to display
    public int initialItemCount = 4;

    // The maximum number of items to display
    public int maxItemCount = 14;

    // The increment in the number of items to display each round
    public int itemIncrement = 1;

    // Set the duration of the FuzzyScreen effect as the difficulty increases
    public float fuzzyScreenDuration = 0.5f;

    // The current number of items to display
    private int currentItemCount;

    // The current round number
    private int currentRound = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial item count
        currentItemCount = initialItemCount;
    }

    // Get the number of items to display in the current round
    public int GetItemCount()
    {
        return currentItemCount;
    }

    // Increment the round number and update the item count
    public void NextRound()
    {
        // Increment the round number
        currentRound++;

        // Increase the number of items to display for the next round
        currentItemCount += itemIncrement;

        // Clamp the item count to the maximum value
        currentItemCount = Mathf.Min(currentItemCount, maxItemCount);

        // Set the duration of the FuzzyScreen effect
        fuzzyScreenDuration = fuzzyScreenDuration * 1.2f;
        fuzzyScreenDuration = Mathf.Min(fuzzyScreenDuration, 3.0f);
        GameObject.Find("FuzzyScreen").GetComponent<FuzzyScreen>().duration = fuzzyScreenDuration;
    }

    // Get the current round number
    public int GetCurrentRound()
    {
        return currentRound;
    }
}
