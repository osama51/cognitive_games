using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public GameManager gameManager;

    private void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(StartGame);     
    }

    private void StartGame()
    {
        gameManager.StartGame();
        //  // Hide the button after pressing it
        // btn.gameObject.SetActive(false);   
    }
}
