using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser2 : Activateable{

    public GameObject dispensedObject;
    public float timeBetweenDispenses;
    public Vector2 spawnPoint;
    //Always shoots out the forward y axis
    public float initialForce;

    //Used to deactivate after x amount have been dispensed. leave at -1 for infinite
    public float maxDispences = -1f;
    private float numDispences = 0;

	// Use this for initialization
	public override void Start () {
        base.Start();
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
                core.GetComponent<Rigidbody2D>().AddForce(transform.right * initialForce);

                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), core.GetComponent<Collider2D>());

                if(maxDispences != -1)
                {
                    numDispences++;
                    if (numDispences >= maxDispences)
                    {
                        numDispences = 0;
                        Deactivate();
                    }
                }

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
