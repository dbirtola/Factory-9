using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour {

    private Vector2 directionToChase;
    public bool isChasing = false;
    public float distance = 10;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if(isChasing == true)
        {
            GetComponent<EnemyMovement>().isPatrolling = false;
        directionToChase = GetComponent<AIController>().target.transform.position - transform.position;
        if (directionToChase.x > 0)//set speed positive
            if (GetComponent<Robot>().speed < 0)//if speed was negative, make it positive
                GetComponent<Robot>().speed = -1 * GetComponent<Robot>().speed;
            else if (directionToChase.x < 0)//set speed negative
                if (GetComponent<Robot>().speed > 0)//if speed was positive, make it negative
                    GetComponent<Robot>().speed = -1 * GetComponent<Robot>().speed;
        GetComponent<RobotController>().MoveHorizontal(GetComponent<Robot>().speed);//start robot with speed 3 to the left towards first patrol point
            if (Vector2.Distance(GetComponent<AIController>().target.transform.position, transform.position) > 10)
            { // if the player get far away, then go back to patrolling
                isChasing = false;
                GetComponent<EnemyMovement>().isPatrolling = true;
            }
            else if (GetComponent<Rigidbody2D>().velocity.x == 0) {
                //enemy robot is stuck, go back to patrol points
            }
        }
    }
}
