using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public Transform[] patrolPoints;//array of patrol points enemy will travel to in order
    Transform currentPatrolPoint;//the current patrol point the enemy is traveling to
    int currentPatrolIndex;//array index counter
    Vector2 patrolPointDirection;//vector in direction of currentPatrolPoint
    private int currentSpeed = -6;
    // Use this for initialization
    void Start () {
        currentPatrolIndex = 0;


    }

    // Update is called once per frame
    void Update () {

        currentPatrolPoint = patrolPoints[currentPatrolIndex];
        patrolPointDirection = currentPatrolPoint.position - transform.position;

        
        if (patrolPointDirection.x > 0)//set speed positive
            if (currentSpeed < 0)//if speed was negative, make it positive
                currentSpeed = -1*currentSpeed;
            else if (patrolPointDirection.x < 0)//set speed negative
                if (currentSpeed > 0)//if speed was positive, make it negative
                    currentSpeed = -1*currentSpeed;
        GetComponent<RobotController>().MoveHorizontal(currentSpeed);//start robot with speed 3 to the left towards first patrol point
        
        //GetComponent<Rigidbody2D>().transform.position
        if (Vector2.Distance(transform.position, currentPatrolPoint.position) <= 0.8)
        {
            Debug.Log("SWITCH DIRECTION");
            //we have reached the patrol point
            //load up next patrol point if we have not reached the last patrol point

            //check to see if we have any more patrol points
            if (currentPatrolIndex + 1 < patrolPoints.Length)
            {

                //move to the next patrol point in the array
                currentPatrolIndex++;//increment index
                currentPatrolPoint = patrolPoints[currentPatrolIndex];//set the new patrol point

                patrolPointDirection = currentPatrolPoint.position - transform.position;//find the new direction
                
                if (patrolPointDirection.x < 0)
                {  //if vector is neg in the x, go left
                    Debug.Log("LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL");

                    currentSpeed = -6;//set speed NEG
                    GetComponent<RobotController>().MoveHorizontal(currentSpeed);
                }
                else if (patrolPointDirection.x > 0)
                { //if vector is pos in the x, go right
                    Debug.Log("RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");

                    currentSpeed = 6;//set speed POS
                    GetComponent<RobotController>().MoveHorizontal(currentSpeed);
                }
                
            }
            else // end of array is reached, loop back through the patrol points
            {
                Debug.Log("ENDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD");

                currentPatrolIndex = 0;

                currentPatrolPoint = patrolPoints[currentPatrolIndex];//set the new patrol point

                patrolPointDirection = currentPatrolPoint.position - transform.position;//find the new direction

                if (patrolPointDirection.x < 0)
                {  //if vector is neg in the x, go left
                    Debug.Log("LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL");

                    currentSpeed = -6;//set speed NEG
                    GetComponent<RobotController>().MoveHorizontal(currentSpeed);
                }
                else if (patrolPointDirection.x > 0)
                { //if vector is pos in the x, go right
                    Debug.Log("RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");

                    currentSpeed = 6;//set speed POS
                    GetComponent<RobotController>().MoveHorizontal(currentSpeed);
                }
                /*
                currentPatrolPoint = patrolPoints[currentPatrolIndex];
                patrolPointDirection = currentPatrolPoint.position - transform.position;
                if (patrolPointDirection.x > 0)//set speed positive
                    if (currentSpeed < 0)//if speed was negative, make it positive
                        currentSpeed = -currentSpeed;
                    else if (patrolPointDirection.x < 0)//set speed negative
                        if (currentSpeed > 0)//if speed was positive, make it negative
                            currentSpeed = -currentSpeed;
                GetComponent<RobotController>().MoveHorizontal(currentSpeed);//start robot with speed 3 to the left towards first patrol point     
                */
            }

            }
        else
        {
            Debug.Log("not close enough yet");
            GetComponent<RobotController>().MoveHorizontal(currentSpeed);//keep moving robot 
        }




    }
}
