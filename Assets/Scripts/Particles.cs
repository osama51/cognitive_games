using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Change Foreground to the layer you want it to display on 
        //You could prob. make a public variable for this
        
        // // set the particle system renderer to the UI layer
        // GetComponent<ParticleSystemRenderer>().sortingLayerName = "UI";

        // stop the particle system from playing at the start
        // GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
