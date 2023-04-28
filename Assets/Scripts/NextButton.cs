using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(NextScene);
        
    }

    private void NextScene()
    {
        // gameManager.StartGame();
        //  // Hide the button after pressing it
        // btn.gameObject.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
