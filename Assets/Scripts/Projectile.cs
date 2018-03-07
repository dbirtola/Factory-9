using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Vector3 destination;
    public float speed;
    Vector3 direction;
    public bool canCutRope = false;

    public bool destroyOnImpact = true;

    void Start()
    {
         direction = (destination - transform.position).normalized;

    }
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Robot>())
        {
            col.gameObject.GetComponent<Robot>().takeDamage(1, gameObject);
        }
        if (destroyOnImpact)
            Destroy(gameObject);
    }
}
