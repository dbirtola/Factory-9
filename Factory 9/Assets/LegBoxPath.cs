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

	}   

    public void GenerateBoxes()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var rp = Instantiate(ropePrefab, transform.GetChild(i).position, Quaternion.identity);
            var bp = Instantiate(boxPrefab, transform.GetChild(i).position - new Vector3(0, 3.0f, 0), Quaternion.identity);
            rp.transform.SetParent(ropeStorage.transform);
            bp.transform.SetParent(boxStorage.transform);


            rp.GetComponent<MovingObjects>().PathObject = gameObject;
            rp.GetComponent<MovingObjects>().startPoint = i + 1;
            if (i == transform.childCount - 1)
                rp.GetComponent<MovingObjects>().startPoint = 0;

            bp.GetComponent<HingeJoint2D>().connectedBody = rp.transform.GetChild(rp.transform.childCount - 1).GetComponent<Rigidbody2D>();


        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
