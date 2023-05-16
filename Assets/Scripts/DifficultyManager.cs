using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    // The initial number of items to display
    public int initialItemCount = 5;

    // The maximum number of items to display
    public int maxItemCount = 17;

    // The increment in the number of items to display each round
    public int itemIncrement = 1;

    // Set the duration of the FuzzyScreen effect as the difficulty increases
    public float fuzzyScreenDuration = 1f;

    // The current number of items to display
    private int currentItemCount;

    // // The current round number
    // private int currentRound = 1;

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

    // // Increment the round number and update the item count
    // public void NextRound()
    // {
    //     // Increment the round number
    //     currentRound++;

    //     // Increase the number of items to display for the next round
    //     currentItemCount += itemIncrement;

    //     // Clamp the item count to the maximum value
    //     currentItemCount = Mathf.Min(currentItemCount, maxItemCount);

    //     // Set the duration of the FuzzyScreen effect
    //     fuzzyScreenDuration = fuzzyScreenDuration * 1.2f;
    //     fuzzyScreenDuration = Mathf.Min(fuzzyScreenDuration, 5.0f);
    //     GameObject.Find("FuzzyScreen").GetComponent<FuzzyScreen>().duration = fuzzyScreenDuration;
    // }

    public void ManageDifficulty(int currenRound)
    {
        currentItemCount = initialItemCount + currenRound - 1;

        // Clamp the item count to the maximum value
        currentItemCount = Mathf.Min(currentItemCount, maxItemCount);

        float baseFuzzyScreenDuration = 1f;
        // Increase the fuzzy screen duration with a factor multiplied by 1.2 to the power of the current round number
        print("CURRENT ROUNDDD: " + currenRound);
        if(currenRound == 1){
            print("fuzzyScreenDuration: " + fuzzyScreenDuration);
            fuzzyScreenDuration = baseFuzzyScreenDuration;
            }
        else
        {fuzzyScreenDuration = baseFuzzyScreenDuration * Mathf.Pow(1.5f, (currenRound - 1));}

        
        fuzzyScreenDuration = Mathf.Min(fuzzyScreenDuration, 6.0f);
        GameObject.Find("FuzzyScreen").GetComponent<FuzzyScreen>().duration = fuzzyScreenDuration;
        print( "fuzzyScreenDuration: " + fuzzyScreenDuration);
        print("roundDuration Before: " + GameObject.Find("GameManager").GetComponent<GameManager>().roundDuration);
        // get the item display time from the scene controller and multiply it by two for the two scenes
        // and assign it as the initial duration of the round
        GameObject.Find("GameManager").GetComponent<GameManager>().roundDuration = GameObject.Find("SceneController").GetComponent<SceneController>().itemDisplayTime * 2;
        // add the new duration of the fuzzy screen to the current duration of the round in the gameManager
        GameObject.Find("GameManager").GetComponent<GameManager>().roundDuration += fuzzyScreenDuration;
        print("roundDuration After: " + GameObject.Find("GameManager").GetComponent<GameManager>().roundDuration);
    }

    // // Get the current round number
    // public int GetCurrentRound()
    // {
    //     return currentRound;
    // }
}
