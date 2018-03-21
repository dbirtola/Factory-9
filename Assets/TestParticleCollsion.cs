using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticleCollsion : MonoBehaviour {

    void OnParticleCollsion(GameObject other)
    {
        Debug.Log(other.name);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
