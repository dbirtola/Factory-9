using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : Activateable {

    public GameObject ropeSegment;
    public int numberOfSections;
    public Vector2 anchorPoint;
    public float mass;
    //Vector2 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GenerateRope()
    {
        foreach (Transform t in transform)
        {
            DestroyImmediate(t.gameObject);
        }



        var initialSeg = Instantiate(ropeSegment, transform.position, Quaternion.identity);
        initialSeg.transform.SetParent(transform);
        initialSeg.transform.localPosition = Vector3.zero;
        DestroyImmediate(initialSeg.GetComponent<HingeJoint2D>());
        initialSeg.AddComponent<FixedJoint2D>();

        GameObject lastSeg = initialSeg;
        for (int i = 1; i <= numberOfSections; i++)
        {
            Vector3 pos = new Vector3(0, i * -0.2f, 0);

            var seg = Instantiate(ropeSegment, pos, Quaternion.identity);
            seg.transform.SetParent(transform);
            seg.transform.localPosition = pos;
            seg.GetComponent<Rigidbody2D>().mass = mass;


            HingeJoint2D hj = seg.GetComponent<HingeJoint2D>();
            hj.anchor = anchorPoint;
            hj.connectedBody = lastSeg.GetComponent<Rigidbody2D>();
            lastSeg = seg;
        }
    }

    public override void Activate()
    {
        base.Activate();
        Destroy(transform.GetChild(transform.childCount / 2).gameObject);
    }
}
