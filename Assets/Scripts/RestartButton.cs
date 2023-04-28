using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(RestartGame);     
        
    }

    private void RestartGame()
    {
        gameManager.StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
