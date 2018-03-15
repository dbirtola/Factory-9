using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPool : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Player>())
        {
            col.gameObject.GetComponent<Robot>().takeDamage(5, gameObject);
            return;
        }
        if (col.GetComponent<Rigidbody2D>() && col.gameObject.layer != LayerMask.NameToLayer("Platform")) 
        {
            Destroy(col.transform.root.gameObject);
        }
    }
}
