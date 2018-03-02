using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : Activateable {

    public GameObject ropeSegment;
    public int numberOfSections;
    public Vector2 anchorPoint;
    public float mass;
    //Vector2 

    public GameObject lastSegment { get; private set; }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public GameObject GetLastSegment()
    {
        return transform.GetChild(transform.childCount - 1).gameObject;
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

        lastSegment = initialSeg;
        //-1 because we already created the intial segment.
        for (int i = 1; i <= numberOfSections -1; i++)
        {
            /*
            Vector3 pos = new Vector3(0, i * -0.2f, 0);

            var seg = Instantiate(ropeSegment, pos, Quaternion.identity);
            seg.transform.SetParent(transform);
            seg.transform.localPosition = pos;
            seg.GetComponent<Rigidbody2D>().mass = mass;


            HingeJoint2D hj = seg.GetComponent<HingeJoint2D>();
            hj.anchor = anchorPoint;
            hj.connectedBody = lastSegment.GetComponent<Rigidbody2D>();
            lastSegment = seg;
            */

            AddSegment();
        }
    }

    public GameObject AddSegment()
    {
        var lastSegment = GetLastSegment();
        Vector3 pos = lastSegment.transform.localPosition + new Vector3(0, -0.2f, 0);

        var seg = Instantiate(ropeSegment, pos, Quaternion.identity);
        seg.transform.SetParent(transform);
        seg.transform.localPosition = pos;
        seg.GetComponent<Rigidbody2D>().mass = mass;



        HingeJoint2D hj = seg.GetComponent<HingeJoint2D>();
        hj.anchor = anchorPoint;
        hj.connectedBody = lastSegment.GetComponent<Rigidbody2D>();

        lastSegment = seg;
        return seg;
    }



    public void DeleteLastSegment()
    {
        DestroyImmediate(lastSegment.gameObject);
        lastSegment = transform.GetChild(transform.childCount - 1).gameObject;
    }



    public override void Activate()
    {
        base.Activate();
        Destroy(transform.GetChild(transform.childCount / 2).gameObject);
    }
}
