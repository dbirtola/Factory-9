using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Activateable : MonoBehaviour
{
    public bool activated;
    virtual public void Activate()
    {
        activated = true;
    }

    virtual public void Deactivate()
    {
        activated = false;
    }
}