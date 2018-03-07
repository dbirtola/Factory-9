using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : BodyPart {

    
    public float pushingSpeed;
    public float pushableMass;

    public GameObject owner;

    public Vector2 punchRange;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void Fire(Vector3 targetLocation)
    {

    }


    public virtual void Punch()
    {
        var anim = GetComponent<Animator>();
        anim.SetTrigger("Punch");

        var colliders = Physics2D.OverlapBoxAll(transform.position + transform.right * transform.lossyScale.x * 0.5f, punchRange, 0);
        foreach(Collider2D c in colliders)
        {
            //Check if owner
            if (c.gameObject == owner)
                continue;


            

            if (c.GetComponent<Robot>())
            {
                c.GetComponent<Robot>().takeDamage(1, gameObject);
            }
            if (c.GetComponent<Destructable>())
            {
                c.GetComponent<Destructable>().takeDamage(1);
            }
        }
    }
    
}
