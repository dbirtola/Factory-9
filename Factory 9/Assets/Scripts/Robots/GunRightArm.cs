using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRightArm : RightArm {

    public GameObject projectile;
    Transform firePoint;

    public Vector2 projectileSpawnPoint;

    float timeFired = -1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(timeFired != -1)
        {
            if(Time.time - timeFired >= 0.5f)
            {
                transform.right = Vector3.right;
            }else
            {
                GetComponent<Animator>().Play("PunchArm");
            }
        }
	}

    public override void Fire(Vector3 targetPosition)
    {
        base.Fire(targetPosition);
        Vector3 temp = new Vector3(targetPosition.x, targetPosition.y, 0);
        transform.right = ( temp - transform.position);

        Vector3 projectilePos = transform.TransformPoint(projectileSpawnPoint);
        var proj = Instantiate(projectile, projectilePos, Quaternion.identity);
        proj.GetComponent<Projectile>().destination = targetPosition;
        Physics2D.IgnoreCollision(proj.GetComponent<Collider2D>(), transform.parent.parent.GetComponent<Collider2D>());
        timeFired = Time.time;


        if (proj.GetComponent<GrapplingHook>())
        {
            proj.GetComponent<GrapplingHook>().objectFiredFrom = gameObject;
        }
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 255);
        Vector3 pos = transform.TransformPoint(projectileSpawnPoint);
        Gizmos.DrawSphere(pos, 0.02f);
    }
}
