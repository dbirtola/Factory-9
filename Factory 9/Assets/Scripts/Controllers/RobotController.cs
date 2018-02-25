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

        //Adding an additional force to make robots drop faster as to improve game feel.
        if(rb.velocity.y < 0)
        {
            //Multiplying by 100 in here so that the addition of Time.deltaTime can be consistent throughout all existing robots
            
            rb.AddForce(new Vector2(0, TheFloatyFeelingFixingFloat * Time.deltaTime * 100));
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


    


    public void FireRightArm(Vector3 targetPosition)
    {
        if(robot.rightArm != null)
        {
            robot.rightArm.Fire(targetPosition);
        }
    }

    public void FireLeftArm(Vector3 targetPosition)
    {
        if(robot.leftArm != null)
        {
            robot.leftArm.Fire(targetPosition);
        }
    }

    public void MoveHorizontal(float speed)
    {
        //transform.position += Vector3.right * speed;
        rb.AddForce(Vector2.right * speed * Time.deltaTime);



        //Check if on ground
        RaycastHit2D hit = Physics2D.Raycast(GetComponent<Collider2D>().bounds.ClosestPoint(transform.position - Vector3.up * 3), Vector3.up * -1, 1f);
        Debug.DrawRay(GetComponent<Collider2D>().bounds.ClosestPoint(transform.position - Vector3.up * 3), Vector3.up * -1, Color.white, 0.1f);
        if (hit.collider != null && hit.distance <= 0.2f)
        {
            state = RobotState.OnGround;
        }

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

    //Rewrote function. Keeping this here in case we need to revert.
    /*
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

            var cone = GetComponentInChildren<VisionCone>();
            if(cone != null)
            {
                var scale = cone.gameObject.transform.localScale;

                cone.gameObject.transform.localScale = new Vector3(-1 * scale.x, scale.y, scale.z);

            }
        }
        if (robot.legs != null && state == RobotState.OnWall)
        {
            robot.legs.GetComponent<SpriteRenderer>().flipX = !shouldFaceLeft;
            transform.Find("Body").GetComponent<SpriteRenderer>().flipX = !shouldFaceLeft;
        }



        isFacingLeft = shouldFaceLeft;
    }
    */


    public void FaceLeft(bool shouldFaceLeft = true)
    {

        if (shouldFaceLeft)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (shouldFaceLeft != isFacingLeft)
        {
            
        }
        
        if(robot.legs != null)
        {
           if(state == RobotState.OnWall)  {
                Debug.Log("Flipping legs");
                robot.legs.GetComponent<SpriteRenderer>().flipX = shouldFaceLeft;
                transform.Find("Body").GetComponent<SpriteRenderer>().flipX = shouldFaceLeft;
            }else
            {
                robot.legs.GetComponent<SpriteRenderer>().flipX = false;
                transform.Find("Body").GetComponent<SpriteRenderer>().flipX = false;

            }
        }



        isFacingLeft = shouldFaceLeft;
    }
    public void Jump()
    {
        if (state != RobotState.InAir)
        {
            Debug.Log("State is: " + state.ToString());
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

            Debug.Log("hit wall");
            
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

            if(robot.legs != null)
            {
                if (col.relativeVelocity.x > 0)
                {
                    FaceLeft();
                }
                else
                {
                    FaceLeft(false);
                }

            }

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
