using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRightArm : RightArm {

    public GameObject projectile;
    Transform firePoint;

    public Vector2 projectileSpawnPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Fire()
    {
        base.Fire();

        Vector2 mousePosition = PlayerController.GetMouseInWorldSpace();
        Vector3 temp = new Vector3(mousePosition.x, mousePosition.y, 0);
        transform.up = -1 *( temp - transform.position);

        Vector3 projectilePos = transform.TransformPoint(projectileSpawnPoint);
        var proj = Instantiate(projectile, projectilePos, Quaternion.identity);
        proj.GetComponent<Projectile>().destination = mousePosition;
        Physics2D.IgnoreCollision(proj.GetComponent<Collider2D>(), transform.parent.parent.GetComponent<Collider2D>());
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 255);
        Vector3 pos = transform.TransformPoint(projectileSpawnPoint);
        Gizmos.DrawSphere(pos, 0.02f);
    }
}
