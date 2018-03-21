using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmSpawner : MonoBehaviour {

    public GameObject armPrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.GetComponent<Destructable>())
        {
            return;
        }

        Instantiate(armPrefab, transform.position- new Vector3(0.5f, 0, 0), Quaternion.identity);

        

    }
}
