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
        //Only allow player, and only if he has no legs
        if (col.gameObject.GetComponent<Player>() == null || col.gameObject.GetComponent<Robot>().legs != null)
            return;

        col.gameObject.GetComponent<PlayerController>().playerRC.isInAirVent = true;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Stealth"), LayerMask.NameToLayer("Platform"));

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Player>() == null)
            return;



        //Use the x axis to determine if the object exited from the left or the right 
        Vector3 vectorToPlayer = (col.gameObject.transform.position - transform.position);
        float angle = Vector2.SignedAngle(transform.right, vectorToPlayer);
        if(angle < 0)
        {

            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), false);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Stealth"), LayerMask.NameToLayer("Platform"), false);
            col.gameObject.GetComponent<PlayerController>().playerRC.isInAirVent = false;
        }

    }
}
