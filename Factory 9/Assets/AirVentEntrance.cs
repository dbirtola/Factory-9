using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirVentEntrance : MonoBehaviour {


    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Player>() == null)
            return;

        Debug.Log("Col : " + col.gameObject);
        Physics2D.IgnoreLayerCollision(col.gameObject.layer, LayerMask.NameToLayer("Platform"));
            /*
        foreach(Collider2D c in colliders)
        {
            if (c.gameObject.layer == LayerMask.NameToLayer("Platform"))
            {
                Physics2D.IgnoreCollision(col, c);
            }
        }
        */
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Player>() == null)
            return;


        //Use the x axis to determine if the object exited from the left or the right 
        Vector3 vectorToPlayer = (col.gameObject.transform.position - transform.position);
        float angle = Vector2.SignedAngle(transform.right, vectorToPlayer);
        Debug.Log(angle);
        if(angle < 0)
        {

            Physics2D.IgnoreLayerCollision(col.gameObject.layer, LayerMask.NameToLayer("Platform"), false);
        }

    }
}
