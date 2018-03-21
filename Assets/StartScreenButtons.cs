using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartScreenButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var t = GetComponent<EventTrigger>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onHover()
    {
        FindObjectOfType<UIHomeScreen>().HoverOver(GetComponent<RectTransform>());
    }
    
}
