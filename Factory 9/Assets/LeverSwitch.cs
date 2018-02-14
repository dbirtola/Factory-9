using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverSwitch : Switch {

    bool hasBeenToggled = false;

    void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 colNorm = col.contacts[0].normal;
        float AngleOfCol = Vector2.Angle(-1 * colNorm, transform.right);
        Debug.Log(AngleOfCol);

        if(AngleOfCol < 90)
        {
            //Debug.Log("NotToggling!");
        }else
        {
            //Debug.Log("Toggling!");
            Toggle();

        }
    }


    public void Toggle()
    {
        Transform leverSwitch = transform.Find("LeverHandle");

        var anim = GetComponent<Animation>();
        if(hasBeenToggled == false)
        {
            anim.Play("BasicLeverToggle");
            activateTargets();
        }else
        {
            anim["BasicLeverToggle"].speed = -1;
            anim["BasicLeverToggle"].time = anim["BasicLeverToggle"].length;
            anim.Play("BasicLeverToggle");
            deactivateTargets();
        }

    }
}
