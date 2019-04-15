using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : Activateable {

    //Prefab for the segments we should use constructing the rope. Should have a HingeJoint2D on them and a RigidBody2D.
    public GameObject ropeSegment;

    //Number of sections to use when calling GenerateRope
    public int numberOfSections;

    //Anchor point for each segments hingejoint to connect to newly created segments
    public Vector2 anchorPoint;

    //Mass to assign to each segment. Increase to make rope break easier
    public float mass;


    public GameObject GetLastSegment()
    {
        if(transform.childCount > 0)
        {
            return transform.GetChild(transform.childCount - 1).gameObject;
        }
        else
        {
            return null;
        }
    }


    //Deletes existing segments and generates a new rope using numberOfSections segments
    public void GenerateRope()
    {
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }


        //Manually create the first segment, as AddSegment assumes lastSegment contains a valid gameobject.
        var initialSeg = Instantiate(ropeSegment, transform.position, Quaternion.identity);
        initialSeg.transform.SetParent(transform);
        initialSeg.transform.localPosition = Vector3.zero;

        //Replace the typical HingeJoint with a FixedJoint so that the rope can be attached to a position in the world.
        DestroyImmediate(initialSeg.GetComponent<HingeJoint2D>());
        initialSeg.AddComponent<FixedJoint2D>();
        
        // -1 because we already created the intial segment.
        for (int i = 0; i < numberOfSections -1; i++)
        {
            AddSegment();
        }
    }

    //Adds a new segment to the end of the rope.
    public GameObject AddSegment()
    {
        var lastSegment = GetLastSegment();

        //In case the designer wants to use the AddSegment button instead of the GenerateRope button to start a rope.
        if(lastSegment == null)
        {
            numberOfSections = 1;
            GenerateRope();
            return GetLastSegment();
        }

        //TODO: Calculate distance between segments based on the rope segment prefab passed in
        Vector3 pos = pos = lastSegment.transform.localPosition + new Vector3(0, -0.2f, 0);

        //Initialize the segment to match the ropes variables
        var seg = Instantiate(ropeSegment, pos, Quaternion.identity);
        seg.transform.SetParent(transform);
        seg.transform.localPosition = pos;
        seg.GetComponent<Rigidbody2D>().mass = mass;


        //Connect the segment to the last segment in the rope
        HingeJoint2D hj = seg.GetComponent<HingeJoint2D>();
        hj.anchor = anchorPoint;
        hj.connectedBody = lastSegment.GetComponent<Rigidbody2D>();
        
        return seg;
    }


    //Removes the most recently aded segment
    public void DeleteLastSegment()
    {
        DestroyImmediate(GetLastSegment());
    }


    //Activating a rope causes it to cut in half, destroying a segment in the center will destroy the
    //physics constraints and release half of the rope
    public override void Activate()
    {
        base.Activate();
        Destroy(transform.GetChild(transform.childCount / 2).gameObject);
    }
}
