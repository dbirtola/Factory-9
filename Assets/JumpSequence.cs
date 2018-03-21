using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSequence : ScriptableSequence {

    public Robot jumpingRobot;

    protected override void Start()
    {
        base.Start();
        sequenceName = "JumpSequence";
    }
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Player>())
        {
            ScriptableSequence.PlaySequenceByName("JumpSequence");
        }

    }


    protected override IEnumerator PlayScriptedSequence()
    {
        PlayerController.playerController.SetMovementEnabled(false);
        RobotController rc = jumpingRobot.GetComponent<RobotController>();
        yield return new WaitForSeconds(2);
        if (jumpingRobot.GetComponentInChildren<RobotHeadLamp>())
        {
            jumpingRobot.GetComponentInChildren<RobotHeadLamp>().TurnOff();
        }
        yield return new WaitForSeconds(1f);
        rc.GetComponent<RobotController>().Jump();
        for(int i = 0; i < 60; i++)
        {
            rc.MoveHorizontal(jumpingRobot.speed * 2.5f);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);
        PlayerController.playerController.SetMovementEnabled(true);

        yield return null;
    }



}
