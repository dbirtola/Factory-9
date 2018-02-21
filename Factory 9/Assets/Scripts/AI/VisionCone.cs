using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour {

    public AIVision owningAIVision;

    void OnTriggerEnter2D(Collider2D col)
    {
        var vis = owningAIVision.GetComponent<AIVision>();
        if (col.gameObject.GetComponent<Player>() && vis != null && col.gameObject.GetComponent<PlayerController>().isStealthed == false)
        {
            Vector2 direction = (col.gameObject.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 10);
            if (hit.collider.gameObject.GetComponent<Player>())
            {

                Debug.Log("player in sight");
                vis.playerSpottedEvent.Invoke(col.gameObject);

            }
        }
    }
}
