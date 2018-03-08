using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEditor;


[CustomEditor(typeof(LegBoxPath))]
public class LegBoxPathEditor : Editor {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var lbp = (LegBoxPath)target;
        if(GUILayout.Button("Generate Rope"))
        {
            lbp.GenerateBoxes();
        }

    }
}
