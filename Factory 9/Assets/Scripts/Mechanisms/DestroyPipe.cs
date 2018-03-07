using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPipe : MonoBehaviour {

    public GameObject[] inputObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {

        for (int i = 0; i < inputObject.Length; i++)
        {
            if (inputObject[i] == null)
                i++;
            else
                if (coll.gameObject.tag == inputObject[i].gameObject.tag)//delete object
            {
                Destroy(inputObject[i]);
                GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }
    }
}
