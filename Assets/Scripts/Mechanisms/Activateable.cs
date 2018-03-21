using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Activateable : MonoBehaviour
{
    public bool activated;


    public virtual void Start()
    {
        if (activated)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }

    virtual public void Activate()
    {
        activated = true;
    }

    virtual public void Deactivate()
    {
        activated = false;
    }
}