using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour {

    public int health = 1;

    public GameObject primaryParticles;
    public GameObject secondaryParticle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void takeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {



            Destroy(gameObject);
        }
    }


    public void OnDestroy()
    {
        if (primaryParticles != null)
        {
            var temp = Instantiate(primaryParticles, transform.position, Quaternion.identity);
            Destroy(temp, 1.5f);
        }
        if (secondaryParticle != null)
        {
            var temp = Instantiate(secondaryParticle, transform.position, Quaternion.identity);
            Debug.Log(temp + " was spawned");
            Destroy(temp, 1.5f);
        }


    }


}
