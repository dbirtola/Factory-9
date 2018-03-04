using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEditor;


[CustomEditor(typeof(AirVent))]
public class AirVentEditor : Editor {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var airVent = (AirVent)target;
        if(GUILayout.Button("Generate Vent"))
        {
            airVent.GenerateCollider();
        }

    }
}
