using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : Activateable{

    public GameObject dispensedObject;
    public float timeBetweenDispenses;
    public Vector2 spawnPoint;
    //Always shoots out the forward y axis
    public float initialForce;

	// Use this for initialization
	void Start () {
        StartCoroutine(dispenseObjects());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator dispenseObjects()
    {
        while (true)
        {
            if (activated)
            {
                Vector3 pos = transform.TransformPoint(spawnPoint);
                var core = Instantiate(dispensedObject, pos, Quaternion.identity);
                core.GetComponent<Rigidbody2D>().AddForce(transform.up * initialForce);
            }


            yield return new WaitForSeconds(timeBetweenDispenses);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 255);
        Vector3 pos = transform.TransformPoint(spawnPoint);
        Gizmos.DrawSphere(pos, 0.06f);
    }


    public override void Activate()
    {
        base.Activate();
        activated = true;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        activated = false;
    }
}
