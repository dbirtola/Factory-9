using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public Transform[] patrolPoints;//array of patrol points enemy will travel to in order
    public float speed;//enemy speed
    Transform currentPatrolPoint;//the current patrol point the enemy is traveling to
    int currentPatrolIndex;//array index counter
    Vector2 patrolPointDirection;//vector in direction of currentPatrolPoint
    // Use this for initialization
    void Start () {
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
        patrolPointDirection = currentPatrolPoint.position - transform.position;
        GetComponent<Rigidbody2D>().AddForce(patrolPointDirection * Time.deltaTime * speed, ForceMode2D.Force);
    }

    // Update is called once per frame
    void Update () {
        //move enemy
        // transform.Translate(Vector2.left * Time.deltaTime * speed);
        //check to see if we have reached the patrol point
        //how far are we from our patrol point?
       

        
        if (Vector2.Distance(transform.position, currentPatrolPoint.position) < 0.1f)
        {
            //we have reached the patrol point
            //load up next patrol point



            //GetComponent<Rigidbody2D>().MovePosition(patrolPointDirection);
            //check to see if we have any more patrol points
            if (currentPatrolIndex+1 < patrolPoints.Length)
            {
                //end of array i
                currentPatrolIndex++;
                patrolPointDirection = currentPatrolPoint.position - transform.position;
                patrolPointDirection.Set(patrolPointDirection.x, 0);
                patrolPointDirection.Normalize();
                GetComponent<Rigidbody2D>().AddForce(patrolPointDirection * Time.deltaTime * speed, ForceMode2D.Force);
            }
            else //if end of array is reached, loop back through the patrol points
            {
                currentPatrolIndex = 0;
            }
            currentPatrolPoint = patrolPoints[currentPatrolIndex];

        }
        
        //turn to face the current patrol point
        //finds which direction to go
        
        patrolPointDirection = currentPatrolPoint.position - transform.position;
        GetComponent<Rigidbody2D>().MovePosition(patrolPointDirection);



        
    }
}
