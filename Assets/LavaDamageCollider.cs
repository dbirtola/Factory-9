using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamageCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter2D(Collider2D col)
    {
        if (transform.parent.GetComponent<Activateable>().activated == false)
            return;
        Debug.Log("ENTERED");


        RaycastHit2D lavaHit = Physics2D.Raycast(transform.parent.position, col.gameObject.transform.position - transform.parent.position, 40);
        Debug.DrawRay(transform.parent.position, Vector2.down * 40);
        if (lavaHit.collider.gameObject == col.gameObject)
        {
            if (col.gameObject.GetComponent<Robot>())
            {
                col.gameObject.GetComponent<Robot>().takeDamage(5, transform.parent.gameObject);
            }
        }
        else
        {
            Debug.Log(lavaHit.collider.gameObject);
        }
    }
}
