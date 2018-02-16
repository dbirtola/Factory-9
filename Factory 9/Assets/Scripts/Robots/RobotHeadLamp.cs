using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHeadLamp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void FlickerLight(bool on = true)
    {
        if (on)
        {
            GetComponent<Animation>().Play("HeadLampFlicker");
        }
    }
}
