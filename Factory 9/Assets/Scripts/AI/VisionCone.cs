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
            //vis.playerSpottedEvent.Invoke(col.gameObject);
            Vector2 direction = (col.gameObject.transform.position - transform.parent.position + Vector3.right * transform.lossyScale.x).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.parent.position + Vector3.right* transform.lossyScale.x * 0.2f, direction, 10);
            Debug.DrawRay(transform.parent.position + Vector3.right * transform.lossyScale.x * 0.2f, direction * 10, Color.green);
            if (hit.collider.gameObject.GetComponent<Player>())
            {

                vis.playerSpottedEvent.Invoke(col.gameObject);

            }
            else
            {
                Debug.Log("Hit : " + hit.collider.gameObject);
                     
            }
        }
    }
}
