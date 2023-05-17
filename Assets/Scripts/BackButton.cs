using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public GameManager gameManager;
    public SceneController sceneController;

    private void Start()
    {
        Button btn = GetComponent<Button>();

        // button is hidden by default
        btn.gameObject.SetActive(false);

        btn.onClick.AddListener(EndGame);     
    }

    private void EndGame()
    {
            // sceneController.EndGame();
            sceneController.GameOver("Lost");
            // gameManager.startGameState = GameManager.StartGameState.Start; 
    }
}
