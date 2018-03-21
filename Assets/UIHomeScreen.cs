using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHomeScreen : MonoBehaviour {
    
    public RectTransform hoverArrow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HoverOver(RectTransform b)
    {
        Vector3 pos = hoverArrow.position;
        pos.y = b.position.y - 11f;
        hoverArrow.position = pos;



    }
}
