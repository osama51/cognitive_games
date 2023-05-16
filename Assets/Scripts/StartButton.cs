using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public GameManager gameManager;
    public SceneController sceneController;

    private void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(StartGame);     
    }

    private void StartGame()
    {
        if(gameManager.startGameState != GameManager.StartGameState.GameOver){
            gameManager.StartGame();
            //  // Hide the button after pressing it
            // btn.gameObject.SetActive(false);  
        } else {
            sceneController.EndGame();
        }
 
    }
}
