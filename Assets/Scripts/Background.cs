using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // set the sorting layer of the background image to the lowest value
        GetComponent<SpriteRenderer>().sortingOrder = -5;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
