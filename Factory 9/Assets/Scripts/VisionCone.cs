using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour {

    public AIVision owningAIVision;

    void OnTriggerEnter2D(Collider2D col)
    {
        var vis = owningAIVision.GetComponent<AIVision>();
        if (col.gameObject.GetComponent<Player>() && vis != null)
        {
            Debug.Log("player in sight");
            vis.playerSpottedEvent.Invoke(col.gameObject);
        }
    }
}
