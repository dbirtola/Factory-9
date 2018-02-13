using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSpottedEvent : UnityEvent<GameObject>{

}

public class AIVision : MonoBehaviour {

    public PlayerSpottedEvent playerSpottedEvent;

    void Awake()
    {
        playerSpottedEvent = new PlayerSpottedEvent();
    }
}
