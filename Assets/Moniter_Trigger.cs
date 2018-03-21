using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moniter_Trigger : Switch
{
    // Use this for initialization
    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<PlayerController>() == null)
        {
            return;
        }

        foreach (Activateable activatable in targetObjects)
        {
            activatable.Activate();
        }
    }



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

