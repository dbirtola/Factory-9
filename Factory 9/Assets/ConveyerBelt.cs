using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : Activateable {

    
    public float speed = 300f;
    public bool rotatingRight = true;
    
    public void Start()
    {
        updateGearSpeeds();
    }

    void updateGearSpeeds()
    {
        foreach (Rotator r in GetComponentsInChildren<Rotator>())
        {
            r.speed = speed / 3;
            if(rotatingRight == true)
            {
                r.speed *= -1f;
            }
        }
    }


    
    //Toggles direction on activate and deactivate
    public override void Activate()
    {
        base.Activate();
        rotatingRight = !rotatingRight;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        rotatingRight = !rotatingRight;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (activated == false)
            return;

        
        Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();

        Vector2 direction = transform.right;
        if (rotatingRight == false)
            direction *= -1;
        //Mass needed so all objects go the same speed
        rb.AddForce(direction * speed * Time.deltaTime * col.gameObject.GetComponent<Rigidbody2D>().mass);
    }


}
