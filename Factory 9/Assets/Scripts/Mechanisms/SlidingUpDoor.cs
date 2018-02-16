using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingUpDoor : Activateable {




    public void Open()
    {
        if(activated == false)
        {
            GetComponent<Animation>().Play("SlidingUpDoorAnimation");
            GetComponent<Collider2D>().isTrigger = true;
        }
    }

    public void Close()
    {
        if(activated == true)
        {
            Animation anim = GetComponent<Animation>();
            anim["SlidingUpDoorAnimation"].speed = -1;
            anim["SlidingUpDoorAnimation"].time = anim["SlidingUpDoorAnimation"].length;
            anim.Play("SlidingUpDoorAnimation");
            GetComponent<Collider2D>().isTrigger = false;

        }
    }

    public override void Activate()
    {
        Open();
        base.Activate();
    }

    public override void Deactivate()
    {
        Close();
        base.Deactivate();
    }

    
}
