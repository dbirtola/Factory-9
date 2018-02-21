using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour {

    private Vector2 directionToChase;
    public bool isChasing = false;
    public float distance = 10;
    private float currentSpeed = 9;

    // Use this for initialization
    void Start () {
	}

    // Update is called once per frame
    void Update() {
        if (isChasing == true)
        {

            GetComponent<EnemyMovement>().isPatrolling = false;
            StartCoroutine(ChasingDirection(.35f));

            if (Vector2.Distance(GetComponent<AIController>().target.transform.position, transform.position) > 10)
            { // if the player get far away, then go back to patrolling
                isChasing = false;
                GetComponent<EnemyMovement>().isPatrolling = true;
                Debug.Log("ESCAPED");
                GetComponent<RobotController>().MoveHorizontal(0);//has no speed
                StartCoroutine(RobotStuck(2f));
            }

            //If robot gets too close to the player, slow down
            if (Vector2.Distance(GetComponent<AIController>().target.transform.position, transform.position) < 2)
            {
                Debug.Log("CONTACT");

                GetComponent<RobotController>().MoveHorizontal(0);//has no speed
                StartCoroutine(RobotStuck(.5f));
                GetComponent<RobotController>().MoveHorizontal(currentSpeed);//has no speed

                //if robot has arms, punch
                //else if robot has a gun, shoot
                //else follow player at a distance
                //   GetComponent<RobotController>().MoveHorizontal((GetComponent<Robot>().speed)/2);
            }
        }
    }

    
    IEnumerator RobotStuck(float pauseTime)
    {
        isChasing = false;
        //pause enemy at stuck position
        yield return new WaitForSeconds(pauseTime);
        isChasing = true;
    }

    IEnumerator ChasingDirection(float pauseTime)
    {
        //pause enemy at stuck position
        directionToChase = GetComponent<AIController>().target.transform.position - GetComponent<Rigidbody2D>().transform.position;
        if (directionToChase.x > 0 && currentSpeed < 0)
        {//set speed positive
            Debug.Log("RIGHT");

            currentSpeed = -1 * currentSpeed;

        }
        else if (directionToChase.x < 0 && currentSpeed > 0)//set speed negative
        {
            Debug.Log("LEFT");

            currentSpeed = -1 * currentSpeed;

        }

        yield return new WaitForSeconds(pauseTime);
       // GetComponent<Rigidbody2D>().velocity.Set(GetComponent<Robot>().speed,0);
       GetComponent<RobotController>().MoveHorizontal(currentSpeed);

    }
}
