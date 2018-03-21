using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class WallSpottedEvent : UnityEvent<GameObject>
{

}

public class AIJump : MonoBehaviour
{

    public WallSpottedEvent WallSpottedEvent;

    void Awake()
    {
        WallSpottedEvent = new WallSpottedEvent();
    }
}
