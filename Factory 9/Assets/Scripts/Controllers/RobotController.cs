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

    public RobotState state = RobotState.Idle;

    GameObject lastSurfaceHit;

    Rigidbody2D rb;
    Robot robot;

    bool canJump = true;
    private float timeAttatchedToWall;

    public float WallJumpBonusPercent = 0.10f;
    public float TheFloatyFeelingFixingFloat = -100f;
    public float wallStickDuration = 0.2f;
    public float WallJumpPushOffPower = 1000f;

    public float WallJumpMovementDisableDuration = 0.5f;

    bool isFacingLeft = false;

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
            if (distanceFromLastSurface.distance >= 0.2f)
            {

                state = RobotState.InAir;
            }
        }

        if(rb.velocity.y < 0)
        {
            rb.AddForce(new Vector2(0, TheFloatyFeelingFixingFloat));
        }

       
    }

    void FixedUpdate()
    {
        if(state == RobotState.OnGround && rb.velocity.magnitude <= 0.2f)
        {
            if(robot.legs != null)
                robot.legs.GetComponent<Animator>().Play("Idle");
            if(robot.rightArm != null)
                robot.rightArm.GetComponent<Animator>().Play("Idle");
            if(transform.Find("Body").GetComponent<Animator>() != null)
            {

                transform.Find("Body").GetComponent<Animator>().Play("Idle");
            }
        }

        if (state == RobotState.OnWall && rb.velocity.y <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (state == RobotState.OnWall && Time.time - timeAttatchedToWall >= wallStickDuration)
        {
            state = RobotState.InAir;
        }

        if (state == RobotState.OnWall && robot.legs != null)
        {
            robot.legs.GetComponent<Animator>().Play("WallJump");

        }
        /*if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }*/
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
        rb.AddForce(Vector2.right * speed * Time.deltaTime);



        //Check if on ground
        RaycastHit2D hit = Physics2D.Raycast(GetComponent<Collider2D>().bounds.ClosestPoint(transform.position - new Vector3(0, 3, 0)), transform.up * -1, 1f);

        if (hit.collider != null && hit.distance <= 0.2f)
        {

            state = RobotState.OnGround;
        }
        Debug.Log(hit.distance);

        //Animations

        if (robot.legs != null && state != RobotState.OnWall)
        {
            var anim = robot.legs.GetComponent<Animator>();
            anim.Play("LegRunAnimation");

            transform.Find("Body").GetComponent<Animator>().Play("HeadBob");


        }
        if(robot.legs != null)
        {
            if (speed > 0)
            {
                FaceLeft(false);
            }
            else if(speed < 0)
            {
                FaceLeft(true);
            }

        }


        if (robot.rightArm != null)
        {
            robot.rightArm.GetComponent<Animator>().Play("RightArmRun");

        }

    }


    public void FaceLeft(bool shouldFaceLeft = true)
    {
        var renderers = GetComponentsInChildren<SpriteRenderer>();

        foreach(SpriteRenderer sr in renderers)
        {
            {
                sr.flipX = shouldFaceLeft;

            }
        }

        if(shouldFaceLeft != isFacingLeft)
        {

            GameObject leftTemp = transform.Find("LeftArmSlot").gameObject;
            GameObject rightTemp = transform.Find("RightArmSlot").gameObject;
            Vector3 posTemp = leftTemp.transform.position;
            int orderTemp = leftTemp.GetComponent<SpriteRenderer>().sortingOrder;
            leftTemp.transform.position = rightTemp.transform.position;
            leftTemp.GetComponent<SpriteRenderer>().sortingOrder = rightTemp.GetComponent<SpriteRenderer>().sortingOrder;
            rightTemp.transform.position = posTemp;
            rightTemp.GetComponent<SpriteRenderer>().sortingOrder = orderTemp;


        }
        if (robot.legs != null && state == RobotState.OnWall)
        {
            Debug.Log("Flipping legs");
            robot.legs.GetComponent<SpriteRenderer>().flipX = !shouldFaceLeft;
            transform.Find("Body").GetComponent<SpriteRenderer>().flipX = !shouldFaceLeft;
        }



        isFacingLeft = shouldFaceLeft;
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
                Vector2 dir = new Vector2();

                if(lastSurfaceHit.transform.position.x - transform.position.x > 0)
                {
                    dir = Vector2.right * -1;
                    PlayerController.playerController.DisableSingleSidedMovementForDuration(WallJumpMovementDisableDuration, true);
                }
                else
                {
                    dir = Vector2.right;
                    PlayerController.playerController.DisableSingleSidedMovementForDuration(WallJumpMovementDisableDuration, false);

                }
                rb.AddForce(dir * WallJumpPushOffPower);
               //PlayerController.playerController.DisableMovementForDuration(WallJumpMovementDisableDuration);
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
            timeAttatchedToWall = Time.time;
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

    
    void OnCollisionStay2D(Collision2D col)
    {
        if(col.contacts.Length <= 0)
        {
            float angleOfCollision = Vector2.Angle(col.contacts[0].normal, Vector2.up);
            if (angleOfCollision < 45)
            {
                HitGround();
            }

        }

    }
    

    void OnCollisionEnter2D(Collision2D col)
    {
        //Still need to do a tag or layer check to see if we hit a surface instead of any object
        //This treats up as 0 degrees. Left and right are 90 degrees.

        if (col.contacts.Length < 1)
            return;

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
    

    public void SetHeadlightOn(bool on = true)
    {
        var light = GetComponentInChildren<RobotHeadLamp>();
        light.FlickerLight(true);
    }

}
