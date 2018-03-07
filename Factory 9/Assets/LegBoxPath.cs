using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegBoxPath : MonoBehaviour {

    public GameObject ropePath;

    public GameObject ropePrefab;
    public GameObject boxPrefab;

    public GameObject ropeStorage;
    public GameObject boxStorage;
        
	// Use this for initialization
	void Start () {
        for (int i = 0; i < transform.childCount; i++)
        {
            var rp = Instantiate(ropePrefab, transform.GetChild(i).position, Quaternion.identity);
            var bp = Instantiate(boxStorage, transform.GetChild(i).position - new Vector3(0, 3.7f, 0), Quaternion.identity);
            rp.transform.SetParent(ropeStorage.transform);
            bp.transform.SetParent(boxStorage.transform);
        }
	}   
	
	// Update is called once per frame
	void Update () {
		
	}
}
