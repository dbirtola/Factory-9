using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSwitch : Switch {

    bool isTriggered = false;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.contacts == null)
            return;
        //This is used to detect if an object hit the top of the switch.
        //-1 because the normal is inverted (not sure why, not good at physics)
        Vector2 normalOfCollision = -1 * col.contacts[0].normal;
        Vector2 worldUp = new Vector2(transform.up.x, transform.up.y);

        if(normalOfCollision == worldUp)
        {
            if(isTriggered == false)
            {
                activateTargets();
                GetComponent<Animation>().Play("BasicSwitchAnim");
                isTriggered = true;
            }
        }
    }
}
