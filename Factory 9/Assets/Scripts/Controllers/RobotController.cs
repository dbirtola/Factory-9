using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RobotState
{
    Idle,       //0
    OnGround,   //1
    InAir,      //2
    OnWall,     //3
    Pushing
}


public class RobotController : MonoBehaviour {

    public RobotState state = RobotState.Idle;

    GameObject lastSurfaceHit;

    Rigidbody2D rb;
    Robot robot;


    bool pushing = false;
    private float timeAttatchedToWall;

    public bool isInAirVent = false;

    //These are variables to improve the feel of the robot
    //Bonus jump height when jumping off a wall
    public float WallJumpBonusPercent = 0.10f;

    //Pulls robot back down from the peak of their jump faster
    public float TheFloatyFeelingFixingFloat = -100f;

    //How long a robot can hold onto the wall
    public float wallStickDuration = 0.2f;

    //Force exerted on a robot from their legs kicking off a wall
    public float WallJumpPushOffPower = 1000f;

    //Time before a robot regains control after jumping off a wall.
    //Used to prevent players who hold a and d while jumping from running back into the wall.
    public float WallJumpMovementDisableDuration = 0.5f;

    bool isFacingLeft = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        robot = GetComponent<Robot>();
    }


    void Update()
    {
        //Update the animator parameters
        if (robot.legs != null)
        {
            var anim = robot.legs.GetComponent<Animator>();
            anim.SetFloat("HorizontalSpeed", rb.velocity.x);
            anim.SetFloat("VerticalSpeed", rb.velocity.y);
            anim.SetInteger("RobotState", (int)state);
            anim.SetBool("Pushing", pushing);
        }
        if (robot.leftArm != null)
        {

        }
        if (robot.rightArm != null)
        {
            var anim = robot.rightArm.GetComponent<Animator>();
            anim.SetFloat("HorizontalSpeed", rb.velocity.x);
            anim.SetInteger("RobotState", (int)state);
            anim.SetBool("Pushing", pushing);
        }




    }

    void FixedUpdate()
    {
        //Considers the robot falling if they simply move off the wall instead of jumping off it
        if (lastSurfaceHit != null)
        {
            var distanceFromLastSurface = lastSurfaceHit.GetComponent<Collider2D>().Distance(GetComponent<Collider2D>());
            //Debug.DrawLine(distanceFromLastSurface.pointA, distanceFromLastSurface.pointB, Color.white);
            if (distanceFromLastSurface.distance >= 0.2f)
            {
                state = RobotState.InAir;
            }
        }

        //Adding an additional force to make robots drop faster as to improve game feel.
        if (state == RobotState.InAir && rb.velocity.y < 0)
        {
            rb.AddForce(new Vector2(0, TheFloatyFeelingFixingFloat * Time.deltaTime * 100));
        }

        //Check if on ground
        RaycastHit2D hit = Physics2D.Raycast(GetComponent<Collider2D>().bounds.ClosestPoint(transform.position - Vector3.up * 3), Vector3.up * -1, 1f);
       // RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.up * -1, 1.5f);

        Debug.DrawRay(transform.position, Vector3.up * -1 * 1.5f, Color.white, 0.1f);
        if (hit.collider != null && hit.distance <= 0.2f)
        {
            state = RobotState.OnGround;
        }

        //Have the robot grab the wall
        if (state == RobotState.OnWall && rb.velocity.y <= 0 && isInAirVent == false)
        {
            if (rb.velocity.y <= 0)
                rb.velocity = new Vector2(rb.velocity.x, 0);

            if (Time.time - timeAttatchedToWall >= wallStickDuration)
                state = RobotState.InAir;

        }


        if (state == RobotState.OnGround && rb.velocity.magnitude <= 0.2f)
        {
            //Spaghetti
            if (transform.Find("Body") == null)
                return;
            
            if (transform.Find("Body").GetComponent<Animator>() != null)
            {
                transform.Find("Body").GetComponent<Animator>().Play("Idle");
            }
        }
    }


    public void FireRightArm(Vector3 targetPosition)
    {
        if (robot.rightArm != null)
        {
            robot.rightArm.Fire(targetPosition);
        }
    }

    public void FireLeftArm(Vector3 targetPosition)
    {
        if (robot.leftArm != null)
        {
            robot.leftArm.Fire(targetPosition);
        }
    }


    public void Punch()
    {
        if (robot.rightArm != null)
        {
            robot.rightArm.Punch();
        } else if (robot.leftArm != null)
        {
            robot.leftArm.Punch();
        }
    }

    //Should move to robot itself not controller
    public bool pushObject(GameObject other)
    {
        Arm strongerArm = null;
        if (state == RobotState.OnGround && (robot.rightArm != null || robot.leftArm != null))
        {
            //Determine the stronger arm
            if (robot.rightArm != null && robot.leftArm != null)
            {
                if (robot.rightArm.pushableMass > robot.leftArm.pushableMass)
                {
                    strongerArm = robot.rightArm;

                }
                else
                {
                    strongerArm = robot.leftArm;
                }
            }
            else
            {
                //Select either if only 1 arm exists
                if (robot.rightArm != null)
                    strongerArm = robot.rightArm;
                if (robot.leftArm != null)
                    strongerArm = robot.leftArm;
            }



        }
        if (strongerArm == null)
            return false;

        //state = RobotState.Pushing;
        
        Vector3 direction;
        if (isFacingLeft)
        {
            direction = Vector3.left;
        }
        else
        {
            direction = Vector3.right;
        }

        Rigidbody2D otherRB = other.gameObject.GetComponent<Rigidbody2D>();
        if (otherRB != null && otherRB.mass < strongerArm.pushableMass)
        {
            //otherRB.AddForce(direction * robot.pushingPower);
            otherRB.velocity = new Vector2(direction.x * strongerArm.pushingSpeed, otherRB.velocity.y);
        }

        return true;

    }

    public void MoveHorizontal(float speed)
    {
        //transform.position += Vector3.right * speed;
        rb.AddForce(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.Raycast(GetComponent<Collider2D>().bounds.ClosestPoint(transform.position - Vector3.up * 3), Vector3.up * -1, 1f);
        Debug.DrawRay(GetComponent<Collider2D>().bounds.ClosestPoint(transform.position - Vector3.up * 3), Vector3.up * -1, Color.white, 0.1f);
        if (hit.collider != null && hit.distance <= 0.2f)
        {
            state = RobotState.OnGround;
        }

        //Asume we arent pushing unless we are running into an object
        pushing = false;
        Debug.DrawRay(GetComponent<Collider2D>().bounds.ClosestPoint(transform.position + transform.right * transform.lossyScale.x * 3f), transform.right * transform.lossyScale.x * 0.2f, Color.white, 0.2f);
        RaycastHit2D pushHit = Physics2D.Raycast(GetComponent<Collider2D>().bounds.ClosestPoint(transform.position - new Vector3(0, 1, 0) + transform.right * transform.lossyScale.x * 3f), transform.right * transform.lossyScale.x, 0.2f);
        if(state == RobotState.OnGround && pushHit.collider != null && pushHit.distance <= 0.1f)
        {
            pushing = pushObject(pushHit.collider.gameObject);
        }


        //Animations

        if (robot.legs != null)
        {
            if (speed > 0)
            {
                FaceLeft(false);
            }
            else if(speed < 0)
            {
                FaceLeft(true);
            }


            if (state == RobotState.OnGround)
            {
                transform.Find("Body").GetComponent<Animator>().Play("HeadBob");
            }
        }
    }
   

    public void FaceLeft(bool shouldFaceLeft = true)
    {

        if (shouldFaceLeft)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        
        if(robot.legs != null)
        {

            
           if(state == RobotState.OnWall)  {

                robot.legs.GetComponent<SpriteRenderer>().flipX = true;
                transform.Find("Body").GetComponent<SpriteRenderer>().flipX = true;
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
            rb.AddForce(Vector2.up * robot.jumpPower);
            if(state == RobotState.OnWall)
            {
                //Adding a bonus force to wall jumps for game feel
                rb.AddForce(Vector2.up * robot.jumpPower * WallJumpBonusPercent);

                //Calculate direction to push away from wall
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

                //Pushes player away from the wall
                rb.AddForce(dir * WallJumpPushOffPower);
            }
            state = RobotState.InAir;
        }
    }

    void HitGround()
    {
        state = RobotState.OnGround;
        if(robot.legs != null)
        {
            var anim = robot.legs.GetComponent<Animator>();
            anim.SetTrigger("HitGround");
        }
    }

    void HitWall(GameObject wall)
    {
        if(lastSurfaceHit != wall)
        {
            timeAttatchedToWall = Time.time;
            state = RobotState.OnWall;
        }

        lastSurfaceHit = wall;
    }

    void HitCeiling()
    {

    }
    
    void OnCollisionStay2D(Collision2D col)
    {
            

        /*
            //This treats up as 0 degrees. Left and right are 90 degrees.
            float angleOfCollision = Vector2.Angle(col.contacts[0].normal, Vector2.up);

            if (angleOfCollision > 45 && angleOfCollision < 100)
            {

                Vector3 direction;
                if (isFacingLeft)
                {
                    direction = Vector3.left;
                }else
                {
                    direction = Vector3.right;
                }
                pushing = true;




                Rigidbody2D otherRb = col.gameObject.GetComponent<Rigidbody2D>();
                if (otherRb != null)
                {
                    
                }
                    
                    //otherRb.AddForce(direction * robot.pushingPower * Time.deltaTime);

            }else
            {
                pushing = false;
            }

        }
        else
        {
            pushing = false;
        }

        

        /*
        if (state == RobotState.OnGround && (robot.rightArm != null || robot.leftArm != null))
        {
            //Debug.Log("PushingL " + pushHit.collider.gameObject);
            //state = RobotState.Pushing;
            pushing = true;
            Vector3 direction;
            if (isFacingLeft)
            {
                direction = Vector3.left;
            }
            else
            {
                direction = Vector3.right;
            }

            Rigidbody2D otherRB = pushHit.collider.gameObject.GetComponent<Rigidbody2D>();
            if (otherRB != null)
            {
                otherRB.AddForce(direction * robot.pushingPower);
            }

        }
        else
        {
            pushing = false;
        }
        */

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.contacts.Length < 1)
            return;

        //This treats up as 0 degrees. Left and right are 90 degrees.
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
