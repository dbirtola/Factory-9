using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Vector3 destination;
    public float speed;
    Vector3 direction;
    public bool canCutRope = false;

    void Start()
    {
         direction = (destination - transform.position).normalized;

    }
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }
}
