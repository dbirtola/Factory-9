using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConverterMachineInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Ingredient>())
        {
            transform.parent.gameObject.GetComponent<ConverterMachine>().receivedIngredient(col.gameObject.GetComponent<Ingredient>());
            Destroy(col.gameObject);
           
        }
    }
}
