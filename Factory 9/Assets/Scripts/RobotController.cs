using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RobotState
{
    Idle,
    OnGround,
    InAir,
    OnWall,
    Climbing
}


public class RobotController : MonoBehaviour {

    RobotState state = RobotState.Idle;

    GameObject lastSurfaceHit;

    Rigidbody2D rb;
    Robot robot;

    bool canJump = true;

    public float WallJumpBonusPercent = 0.10f;
    public float TheFloatyFeelingFixingFloat = -100f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        robot = GetComponent<Robot>();
    }


    void Update()
    {
        if(lastSurfaceHit != null)
        {
            var distanceFromLastSurface = lastSurfaceHit.GetComponent<Collider2D>().Distance(GetComponent<Collider2D>());
            //Debug.DrawLine(distanceFromLastSurface.pointA, distanceFromLastSurface.pointB, Color.white);
            if (distanceFromLastSurface.distance >= 0.2)
            {
                state = RobotState.InAir;
            }
        }

        if(rb.velocity.y <= 0)
        {
            rb.AddForce(new Vector2(0, TheFloatyFeelingFixingFloat));
        }

       
    }

    public void GrabClimeableObject()
    {
    }

    public void FireRightArm()
    {
        if(robot.rightArm != null)
        {
            robot.rightArm.Fire();
        }
    }

    public void FireLeftArm()
    {
        if(robot.leftArm != null)
        {
            robot.leftArm.Fire();
        }
    }

    public void MoveHorizontal(float speed)
    {
        //transform.position += Vector3.right * speed;
        rb.AddForce(Vector2.right * speed);
    }

    public void Jump()
    {
        if (state != RobotState.InAir)
        {
            canJump = false;
            rb.AddForce(Vector2.up * robot.jumpPower);
            if(state == RobotState.OnWall)
            {
                rb.AddForce(Vector2.up * robot.jumpPower * WallJumpBonusPercent);
            }
            state = RobotState.InAir;
        }
    }

    void HitGround()
    {
        canJump = true;
        state = RobotState.OnGround;
    }

    void HitWall(GameObject wall)
    {
        if(lastSurfaceHit != wall)
        {
            canJump = true;
            state = RobotState.OnWall;
        }

        lastSurfaceHit = wall;
    }

    void HitCeiling()
    {

    }


    void PlayerDetected()
    {
        Debug.Log("Player detected!");
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        //Still need to do a tag or layer check to see if we hit a surface instead of any object
        //This treats up as 0 degrees. Left and right are 90 degrees.


        float angleOfCollision = Vector2.Angle(col.contacts[0].normal, Vector2.up);

        if (angleOfCollision < 45)
        {
            HitGround();
        }
        else if (angleOfCollision < 100)
        {
            HitWall(col.gameObject);
        }
        else
        {
            HitCeiling();
        }
        lastSurfaceHit = col.gameObject;

    }



}
