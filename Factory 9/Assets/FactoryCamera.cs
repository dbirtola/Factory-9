using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryCamera : MonoBehaviour {
    public GameObject target;
    public Vector2 offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        Debug.Log("ABAA");
        Vector3 newPosition = new Vector3();
        //Set x and y to the targets x and y
        newPosition.x = target.transform.position.x + offset.x;
        newPosition.y = target.transform.position.y + offset.y;
        newPosition.z = transform.position.z; //Using the cameras z at all times.

        transform.position = newPosition;
    }
}
