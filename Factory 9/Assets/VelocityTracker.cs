using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityTracker : MonoBehaviour {

    public Vector2 velocity;

    


    void FixedUpdate()
    {
        if (GetComponent<Rigidbody2D>())
        {
            velocity = GetComponent<Rigidbody2D>().velocity;
        }
    }
}
