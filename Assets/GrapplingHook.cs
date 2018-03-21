using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour {

    //Set in inspector
    public Rope attatchedRope;

    public GameObject objectFiredFrom;

    void Start()
    {

        if(attatchedRope == null)
        {
            Debug.LogWarning("There is no attatchedRope to the grapplingHook. Set the attatchedRope in the inspector, not at runtime");
        }else
        {
            attatchedRope.transform.localPosition = transform.position;

        }
    }

    void FixedUpdate()
    {
        GameObject lastSegment = attatchedRope.transform.GetChild(attatchedRope.transform.childCount - 1).gameObject;
        attatchedRope.transform.position = transform.position;
       if((lastSegment.transform.position - objectFiredFrom.transform.position).magnitude >= 0.2f)
        {
            Debug.Log("Distance too far, creating new segment!");
            var newSeg = attatchedRope.AddSegment();
            //objectFiredFrom.GetComponent<FixedJoint2D>().connectedBody = newSeg.GetComponent<Rigidbody2D>();

        }
        attatchedRope.GetLastSegment().transform.position = objectFiredFrom.GetComponent<GunRightArm>().projectileSpawnPoint;
    }
   

    void OnCollisionEnter2D(Collision2D col)
    {
        GetComponent<Projectile>().enabled = false;
        FixedJoint2D fj = gameObject.AddComponent<FixedJoint2D>();
        fj.connectedBody = col.gameObject.GetComponent<Rigidbody2D>();

        
    }
}
