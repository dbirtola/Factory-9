using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects : MonoBehaviour
{

    //Path Variables
    public GameObject PathObject;//Parent contains children nodes with Waypoints
    private Transform[] Waypoints;
    private int NumOfWaypoints;
    private int currentPatrolIndex = 0;
    private Transform currentPatrolPoint;

    //movement Variables
    public float speed;//Speed of the moving object
    private Vector2 VelocityDirectionAndMagnitude;
    private Vector2 WayPointDirection;

    // Use this for initialization
    void Start()
    {
        NumOfWaypoints = PathObject.transform.childCount;//Set num of Waypoints
        for (int i = 0; i < NumOfWaypoints; i++)
        {
            Waypoints[i] = transform.GetChild(i);//store array with transforms of Children
        }
        if (NumOfWaypoints <= 0)//If there are no waypoints, RETURN
            return;
    }

    // Update is called once per frame
    void FixedUpdate()
    {



        if (Waypoints.Length <= 0)
            return;

        currentPatrolPoint = Waypoints[currentPatrolIndex];//set patrol point
        WayPointDirection = currentPatrolPoint.position - transform.position;//find the direction to travel


        WayPointDirection.Normalize();
        VelocityDirectionAndMagnitude = speed * WayPointDirection;





        //Set the Direction
        GetComponent<Rigidbody2D>().velocity.Set(VelocityDirectionAndMagnitude.x, VelocityDirectionAndMagnitude.y);



        if (Vector2.Distance(transform.position, currentPatrolPoint.position) <= 1.2)
        {



            //check to see if we have any more patrol points
            if (currentPatrolIndex + 1 < Waypoints.Length)
            {

                currentPatrolIndex++;//increment index
                currentPatrolPoint = Waypoints[currentPatrolIndex];//set the new patrol point

                WayPointDirection = currentPatrolPoint.position - transform.position;//find the new direction

                if (WayPointDirection.x > 0 && speed < 0)//set speed positive if speed was negative
                    speed = -1 * speed;
                else if (WayPointDirection.x < 0 && speed > 0)// speed was positive, make it negative
                    speed = -1 * speed;

            }
            else // end of array is reached, loop back through the patrol points
            {

                currentPatrolIndex = 0;

                currentPatrolPoint = Waypoints[currentPatrolIndex];//set the new patrol point

                WayPointDirection = currentPatrolPoint.position - transform.position;//find the new direction

                if (WayPointDirection.x < 0)
                {  //if vector is neg in the x, go left
                    speed = -6;//set speed NEG
                    GetComponent<RobotController>().MoveHorizontal(speed);
                }
                else if (WayPointDirection.x > 0)
                { //if vector is pos in the x, go right

                    speed = 6;//set speed POS
                    GetComponent<RobotController>().MoveHorizontal(speed);
                }
                GetComponent<RobotController>().MoveHorizontal(speed);


            }

        }
        else
        {
            GetComponent<RobotController>().MoveHorizontal(speed);//keep moving robot 
        }





    }

    void FindDirection()
    {
        //Finds the Direction in which to Travel
        if (WayPointDirection.x > 0 && VelocityDirectionAndMagnitude.x < 0)//set speed positive if speed was negative
            VelocityDirectionAndMagnitude.x = -1 * VelocityDirectionAndMagnitude.x;
        else if (WayPointDirection.x < 0)// && currentSpeed > 0)// speed was positive, make it negative
            VelocityDirectionAndMagnitude.x = -1 * VelocityDirectionAndMagnitude.x;

        if (WayPointDirection.y > 0 && VelocityDirectionAndMagnitude.y < 0)//set speed positive if speed was negative
            VelocityDirectionAndMagnitude.y = -1 * VelocityDirectionAndMagnitude.y;
        else if (WayPointDirection.y < 0)// && currentSpeed > 0)// speed was positive, make it negative
            VelocityDirectionAndMagnitude.y = -1 * VelocityDirectionAndMagnitude.y;
    }

}