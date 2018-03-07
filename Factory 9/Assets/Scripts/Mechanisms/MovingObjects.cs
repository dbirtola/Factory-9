using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects : MonoBehaviour
{

    //Wait time at positions
    public float waitTime;

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
    private bool isWaiting = false;

    // Use this for initialization
    void Start()
    {
        NumOfWaypoints = PathObject.transform.childCount;//Set num of Waypoints
        Waypoints = new Transform[NumOfWaypoints];

        for (int i = 0; i < NumOfWaypoints; i++)
        {
            Waypoints[i] = PathObject.transform.GetChild(i);//store array with transforms of Children
        }
        if (NumOfWaypoints <= 0)//If there are no waypoints, RETURN
            return;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isWaiting == false)
        {



            currentPatrolPoint = Waypoints[currentPatrolIndex];//set patrol point
            WayPointDirection = currentPatrolPoint.position - transform.position;//find the direction to travel

            WayPointDirection.Normalize();
            VelocityDirectionAndMagnitude = speed * WayPointDirection;





            //Set the Direction
            FindDirection();

            GetComponent<Rigidbody2D>().velocity = new Vector2(VelocityDirectionAndMagnitude.x, VelocityDirectionAndMagnitude.y);



            if (Vector2.Distance(transform.position, currentPatrolPoint.position) <= 1.2)
            {

                StartCoroutine(WaitAtWayPoint(waitTime));


                //check to see if we have any more patrol points
                if (currentPatrolIndex + 1 < Waypoints.Length)
                {
                    currentPatrolIndex++;//increment index
                    currentPatrolPoint = Waypoints[currentPatrolIndex];//set the new patrol point
                    WayPointDirection = currentPatrolPoint.position - transform.position;//find the new direction
                    FindDirection();
                }
                else // end of array is reached, loop back through the patrol points
                {
                    currentPatrolIndex = 0;
                    currentPatrolPoint = Waypoints[currentPatrolIndex];//set the new patrol point
                    WayPointDirection = currentPatrolPoint.position - transform.position;//find the new direction
                    FindDirection();
                }
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(VelocityDirectionAndMagnitude.x, VelocityDirectionAndMagnitude.y);


            }
        }
    }

    void FindDirection()
    {
        //Finds the Direction in which to Travel
        if (WayPointDirection.x > 0 && VelocityDirectionAndMagnitude.x < 0)//set speed positive if speed was negative
            VelocityDirectionAndMagnitude.x *= -1;
        else if (WayPointDirection.x < 0 && VelocityDirectionAndMagnitude.x > 0)// speed was positive, make it negative
            VelocityDirectionAndMagnitude.x *= -1;

        if (WayPointDirection.y > 0 && VelocityDirectionAndMagnitude.y < 0)//set speed positive if speed was negative
            VelocityDirectionAndMagnitude.y *= -1;
        else if (WayPointDirection.y < 0 && VelocityDirectionAndMagnitude.y > 0)// speed was positive, make it negative
            VelocityDirectionAndMagnitude.y *= -1;


    }
    IEnumerator WaitAtWayPoint(float waitTime)
    {
        Debug.Log("WAITT");
        isWaiting = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(VelocityDirectionAndMagnitude.x, VelocityDirectionAndMagnitude.y);

    }
}