using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Rope))]
public class RopeCustomEditor : Editor {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var rope = (Rope)target;
        if(GUILayout.Button("Generate Rope"))
        {
            rope.GenerateRope();
        }
    }
}
