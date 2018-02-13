using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {



    public Activateable[] targetObjects;


    virtual public void activateTargets()
    {
        foreach(Activateable ia in targetObjects)
        {
            ia.Activate();
        }
    }

    virtual public void deactivateTargets()
    {
        foreach (Activateable ia in targetObjects)
        {
            ia.Deactivate();
        }
    }
}
