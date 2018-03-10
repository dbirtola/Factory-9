using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor : Activateable {

    public GameObject textMesh;
    public GameObject light;


	// Use this for initialization
	public override void Start () {
        base.Start();
        if (activated)
        {
            textMesh.SetActive(true);
            light.SetActive(true);
        }else
        {
            textMesh.SetActive(false);
            light.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public override void Activate()
    {
        base.Activate();
        textMesh.SetActive(true);
        light.SetActive(true);
       
    }

    public override void Deactivate()
    {
        base.Deactivate();
        textMesh.SetActive(false);
        light.SetActive(false);
    }
}
