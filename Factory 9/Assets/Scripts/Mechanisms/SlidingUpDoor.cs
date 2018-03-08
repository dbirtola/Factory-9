using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingUpDoor : Activateable {
    private AudioSource audioSource;

    public override void Start()
    {
        if (activated)
        {
            Open();
        }else
        {
            Close();
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void Open()
    {
        Animation anim = GetComponent<Animation>();
        anim["SlidingUpDoorAnimation"].speed = 1;
        anim["SlidingUpDoorAnimation"].time = 0;
        anim.Play("SlidingUpDoorAnimation");
        GetComponent<Collider2D>().isTrigger = true;
        
    }

    public void Close()
    {
        Animation anim = GetComponent<Animation>();
        anim["SlidingUpDoorAnimation"].speed = -1;
        anim["SlidingUpDoorAnimation"].time = anim["SlidingUpDoorAnimation"].length;
        anim.Play("SlidingUpDoorAnimation");
            
        GetComponent<Collider2D>().isTrigger = false;
      
    }

    public override void Activate()
    {
        if (!activated)
        {
            Open();
        }
        base.Activate();
    }

    public override void Deactivate()
    {
        audioSource.Play();
        if(activated)
            Close();
        base.Deactivate();
    }

    
}
