using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    public GameObject target;

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
        //chase the player
        target = player.gameObject;
        GetComponent<AIChase>().isChasing = true;

    }
}
