using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {
    

    void Start()
    {
        var vis = GetComponent<AIVision>();
        if(vis != null)
        {
            vis.playerSpottedEvent.AddListener(OnPlayerSpotted);

        }
    }

    void OnPlayerSpotted(GameObject player)
    {
        Debug.Log("Spotted Player");
    }
}
