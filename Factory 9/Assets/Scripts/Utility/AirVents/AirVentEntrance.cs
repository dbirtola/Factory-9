using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*Intended to be used to mark entrance and exits of air vents. No other scripts are needed, simply place the air vent wall prefabs to create
 * a vent and ensure the an entrance script is on both sides so the players collision channels and air vent boolean are updated
 */
public class AirVentEntrance : MonoBehaviour {

    //When the player enters an air vent entrance, we will switchm them over to "InAirVent" mode and change their collision channels so they can go through walls
    void OnTriggerEnter2D(Collider2D col)
    {
        //Only allow player, and only if he is in ball form
        if (col.gameObject.GetComponent<Player>() == null || col.gameObject.GetComponent<Robot>().getNumberOfParts() > 0)
            return;

        col.gameObject.GetComponent<PlayerController>().playerRC.isInAirVent = true;

        //Update the players collision channels to ignore walls
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Stealth"), LayerMask.NameToLayer("Platform"));

    }


    /*When the player leaves an air vent entrance, we check the angle to see if they are in the air vent or leaving the air vent.
     * This assumes the designer has placed the air vent such that the local X axis is facing the exit
     * 
     */
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Player>() == null)
            return;



        /*Compare air vent's x axis and the player's location to determine if the object exited the air vent or entered it. 
         * Negative angles between the collider and player indicate leaving an air vent
         */
        Vector3 vectorToPlayer = (col.gameObject.transform.position - transform.position);
        float angle = Vector2.SignedAngle(transform.right, vectorToPlayer);
        if(angle < 0)
        {
            //Update collision masks so the player can collide with walls again
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), false);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Stealth"), LayerMask.NameToLayer("Platform"), false);
            col.gameObject.GetComponent<PlayerController>().playerRC.isInAirVent = false;
        }

    }
}
