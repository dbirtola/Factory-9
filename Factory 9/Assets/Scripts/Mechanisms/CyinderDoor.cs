using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyinderDoor : Switch {

   // public Activateable[] targetObjects;
    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Cylinder")
        {


            foreach (Activateable activatable in targetObjects)
            {
                activatable.Activate();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Cylinder")
        {


            foreach (Activateable activatable in targetObjects)
            {
                activatable.Activate();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Cylinder")
        {

            foreach (Activateable activatable in targetObjects)
            {
                activatable.Deactivate();
            }
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
