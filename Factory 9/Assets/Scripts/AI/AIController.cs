using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {
    


    protected virtual void Start()
    {
        var vis = GetComponent<AIVision>();
        if(vis != null)
        {
            vis.playerSpottedEvent.AddListener(OnPlayerSpotted);

        }
    }

    protected virtual void OnPlayerSpotted(GameObject player)
    {
        Debug.Log("Spotted Player");
    }
}
