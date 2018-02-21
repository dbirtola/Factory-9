using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public bool isPatrolling = true;//if enemy sees the player, then set this bool False
    public Transform[] patrolPoints;//array of patrol points enemy will travel to in order
    private Transform currentPatrolPoint;//the current patrol point the enemy is traveling to
    private int currentPatrolIndex;//array index counter
    private Vector2 patrolPointDirection;//vector in direction of currentPatrolPoint
    private float currentSpeed = 6;
    private Vector2 stoppingForce = new Vector2(0, 0);
    // Use this for initialization
    void Start () {
        currentPatrolIndex = 0;

        
        for(int i=0;i<patrolPoints.Length;i++)
        {
            if (patrolPoints[i] == null)
            {
                isPatrolling = false;
                break;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isPatrolling)
        {

            currentPatrolPoint = patrolPoints[currentPatrolIndex];//set patrol point
            patrolPointDirection = currentPatrolPoint.position - transform.position;//find the direction to travel


            if (patrolPointDirection.x > 0 && currentSpeed < 0)//set speed positive if speed was negative
                    currentSpeed = -1 * currentSpeed;
                else if (patrolPointDirection.x < 0 && currentSpeed > 0)// speed was positive, make it negative
                        currentSpeed = -1 * currentSpeed;

            GetComponent<RobotController>().MoveHorizontal(currentSpeed);

            //GetComponent<Rigidbody2D>().transform.position
            if (Vector2.Distance(transform.position, currentPatrolPoint.position) <= 1.2)
            {
                GetComponent<Rigidbody2D>().velocity = stoppingForce;
                StartCoroutine(PausePatrol(2));
                //we have reached the patrol point
                //load up next patrol point if we have not reached the last patrol point

               
                //check to see if we have any more patrol points
                if (currentPatrolIndex + 1 < patrolPoints.Length)
                {

                    //move to the next patrol point in the array
                    currentPatrolIndex++;//increment index
                    currentPatrolPoint = patrolPoints[currentPatrolIndex];//set the new patrol point

                    patrolPointDirection = currentPatrolPoint.position - transform.position;//find the new direction

                    if (patrolPointDirection.x > 0 && currentSpeed < 0)//set speed positive if speed was negative
                        currentSpeed = -1 * currentSpeed;
                    else if (patrolPointDirection.x < 0 && currentSpeed > 0)// speed was positive, make it negative
                        currentSpeed = -1 * currentSpeed;

                }
                else // end of array is reached, loop back through the patrol points
                {

                    currentPatrolIndex = 0;

                    currentPatrolPoint = patrolPoints[currentPatrolIndex];//set the new patrol point

                    patrolPointDirection = currentPatrolPoint.position - transform.position;//find the new direction

                    if (patrolPointDirection.x < 0)
                    {  //if vector is neg in the x, go left
                        currentSpeed = -6;//set speed NEG
                        GetComponent<RobotController>().MoveHorizontal(currentSpeed);
                    }
                    else if (patrolPointDirection.x > 0)
                    { //if vector is pos in the x, go right

                        currentSpeed = 6;//set speed POS
                        GetComponent<RobotController>().MoveHorizontal(currentSpeed);
                    }
                    GetComponent<RobotController>().MoveHorizontal(currentSpeed);


                }

            }
            else
            {
                GetComponent<RobotController>().MoveHorizontal(currentSpeed);//keep moving robot 
            }




        }
    }

    IEnumerator PausePatrol(float pauseTime) {
        isPatrolling = false;
        //pause enemy at patrol point
        yield return new WaitForSeconds(pauseTime);
        isPatrolling = true;
    }

}
