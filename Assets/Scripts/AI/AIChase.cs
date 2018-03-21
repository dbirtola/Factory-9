﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour {

    private Vector2 directionToChase;
    public bool isChasing = false;
    private float currentSpeed;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update() {
        if (isChasing == true && GetComponent<Robot>().legs != null)
        {
            //Set Head lamp to Chase mode
            if (GetComponentInChildren<RobotHeadLamp>())
            {
                ChaseHeadLamp(true);
                // GetComponentInChildren<RobotHeadLamp>().chase = true;
            }


            currentSpeed = GetComponent<Robot>().speed;
            GetComponent<EnemyMovement>().isPatrolling = false;
            StartCoroutine(ChasingDirection(.35f));

            if (Vector2.Distance(GetComponent<AIController>().target.transform.position, transform.position) > 12)
            { // if the player get far away, then go back to patrolling
                isChasing = false;
                GetComponent<EnemyMovement>().isPatrolling = true;
                // GetComponent<RobotController>().MoveHorizontal(0);//has no speed
                // StartCoroutine(RobotStuck(2f));
            }

            //If robot gets too close to the player, slow down
            if (Vector2.Distance(GetComponent<AIController>().target.transform.position, transform.position) < 1)
            {

                // GetComponent<RobotController>().MoveHorizontal(0);//has no speed
                StartCoroutine(RobotStuck(.45f));
                GetComponent<RobotController>().MoveHorizontal(0);//has no speed

                //if robot has arms, punch
                //else if robot has a gun, shoot
                //else follow player at a distance
                //   GetComponent<RobotController>().MoveHorizontal((GetComponent<Robot>().speed)/2);
            }
        }
        else if (GetComponent<Robot>().legs == null && GetComponentInChildren<RobotHeadLamp>())//If robot has no legs, turn off head lamp
            GetComponentInChildren<RobotHeadLamp>().TurnOff(true);
        else if (isChasing == false && GetComponentInChildren<RobotHeadLamp>())
            ChaseHeadLamp(false);

    }

    public void ChaseHeadLamp(bool chase = false)
    {
        if (chase)
        {
            GetComponentInChildren<RobotHeadLamp>().SetColor(GetComponentInChildren<RobotHeadLamp>().chaseColor);
            GetComponentInChildren<RobotHeadLamp>().headLight.intensity = 100f;
            GetComponentInChildren<RobotHeadLamp>().headLight.spotAngle = 35;
            // StartCoroutine(Flicker(.5f));

        }
        else
        {
            GetComponentInChildren<RobotHeadLamp>().SetColor(GetComponentInChildren<RobotHeadLamp>().defaultColor);
            GetComponentInChildren<RobotHeadLamp>().headLight.intensity = 37.85f;
            GetComponentInChildren<RobotHeadLamp>().headLight.spotAngle = 27;
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

            currentSpeed = -1 * currentSpeed;

        }
        else if (directionToChase.x < 0 && currentSpeed > 0)//set speed negative
        {

            currentSpeed = -1 * currentSpeed;

        }
        GetComponent<RobotController>().MoveHorizontal(currentSpeed);

        yield return new WaitForSeconds(pauseTime);
       // GetComponent<Rigidbody2D>().velocity.Set(GetComponent<Robot>().speed,0);
       GetComponent<RobotController>().MoveHorizontal(currentSpeed);

    }
}
