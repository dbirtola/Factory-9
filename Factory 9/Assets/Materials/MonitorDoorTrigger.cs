using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorDoorTrigger : Moniter_Trigger{

    public SlidingUpDoor door;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<PlayerController>() == null)
        {
            return;
        }

        foreach (Activateable activatable in targetObjects)
        {
            activatable.Activate();
            if(door != null)
                door.Deactivate();
        }
    }
}
