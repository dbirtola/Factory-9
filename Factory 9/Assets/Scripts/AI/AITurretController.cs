using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITurretController : MonoBehaviour {

    public float angle;
    public float TurretSpeed;
    private bool playerSpotted = false;
    private Vector2 directionToChase;

    // Update is called once per frame
    void FixedUpdate()
    {

        ScanArea();
    }
    protected virtual void Start()
    {

        var vis = GetComponent<AIVision>();
        if (vis != null)
        {
            vis.playerSpottedEvent.AddListener(OnPlayerSpotted);

        }
    }

    protected virtual void OnPlayerSpotted(GameObject player)
    {
        //follow player with light
        //increase spotlight power
        //If player reaches max distance, go back to scanning area with spotlight
        playerSpotted = true;

    }
    public void ScanArea() {

        if (playerSpotted == false)
        {
                if (angle - GetComponent<Rigidbody2D>().rotation > 5 || (angle < 0 && angle - GetComponent<Rigidbody2D>().rotation > -5))
                {
                    if (angle < 0)
                        angle *= -1;
                    // transform.Rotate(Vector3.forward);
                    GetComponent<Rigidbody2D>().MoveRotation(Mathf.LerpAngle(GetComponent<Rigidbody2D>().rotation, angle, t: TurretSpeed * Time.deltaTime));
                }
                else if (angle - GetComponent<Rigidbody2D>().rotation < 5)
                {
                    if (angle > 0)
                        angle *= -1;
                    //   transform.Rotate(Vector3.back);
                    GetComponent<Rigidbody2D>().MoveRotation(Mathf.LerpAngle(GetComponent<Rigidbody2D>().rotation, angle, t: TurretSpeed * Time.deltaTime));
                }
                // Debug.Log(GetComponent<Rigidbody2D>().rotation);

                //transform.Rotate(Vector3.forward);
            }
        }

    
    IEnumerator ShootAtPlayer(float timePerShot)
    {
        directionToChase = GetComponent<AIController>().target.transform.position - GetComponent<Rigidbody2D>().transform.position;

        while (GetComponent<Robot>().rightArm != null && GetComponent<AIChase>().isChasing == true)
        {
            yield return new WaitForSeconds(timePerShot);
          //  GetComponent<Robot>().rightArm.Fire(Target.GetComponent<Robot>().transform.position);
        }
    }
    
}
