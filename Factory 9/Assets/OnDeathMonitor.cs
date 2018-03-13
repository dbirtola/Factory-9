using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathMonitor : Monitor{

    public GameObject secondaryTextMesh;
    public GameObject robot;
	// Use this for initialization
	public override void Start () {
        base.Start();
        
        robot.GetComponent<Robot>().robotDamagedEvent.AddListener(flipText);
	}
	
    void flipText(GameObject robot)
    {
        textMesh.SetActive(false);
        secondaryTextMesh.SetActive(true);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
