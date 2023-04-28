using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelector : MonoBehaviour
{
    private SceneController sceneController;
    public int itemIndex;

    private void Start()
    {
        // Get reference to SceneController script
        sceneController = FindObjectOfType<SceneController>();
    }

    // Called when an item is clicked
    public void OnItemClick(int itemIndex)
    {
        // // print the index of the item that was clicked
        // print("Item " + itemIndex + " was clicked");
        // // Pass the index of the selected item to the SceneController
        // sceneController.CheckAnswer(itemIndex);
        
        SceneController sceneController = FindObjectOfType<SceneController>();
        print("Item " + transform.GetSiblingIndex() + " was clicked");
        if (sceneController)
        {
            sceneController.CheckAnswer(transform.GetSiblingIndex()); // Get the index of the clicked item
        }
    }


}
