using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRobotController : AIController {

	// Use this for initialization
	protected override void Start () {
        base.Start();
	}

    protected override void OnPlayerSpotted(GameObject player)
    {
        base.OnPlayerSpotted(player);

        //Enter code here for when the robot finds a player.
        //shoot the player
        if (target.GetComponent<Robot>().rightArm != null)
        {
            GetComponent<GunRightArm>().Fire(target.GetComponent<Robot>().transform.position);
        }
    }
}
