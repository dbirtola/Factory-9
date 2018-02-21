using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRobotController : AIController
{
    private GameObject Target;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnPlayerSpotted(GameObject player)
    {
        Target = player;
        base.OnPlayerSpotted(player);
        StartCoroutine(ShootAtPlayer(4));

    }
    void Update()
    {
        if (GetComponent<Robot>().rightArm != null && GetComponent<AIChase>().isChasing == true)
        {
            OnPlayerSpotted(Target);

        }
    }
    IEnumerator ShootAtPlayer(float timePerShot)
    {

        yield return new WaitForSeconds(timePerShot);

        GetComponent<Robot>().rightArm.Fire(Target.GetComponent<Robot>().transform.position);

    }

}