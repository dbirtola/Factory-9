using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverSwitch : Switch {

    bool initialPosition = true;

    void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 colNorm = col.contacts[0].normal;
        float AngleOfCol = Vector2.Angle( colNorm, transform.right);

        //Debug.DrawRay(col.contacts[0].point, col.contacts[0].normal * 15, Color.white, 1f);
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
        if(initialPosition == true)
        {
            anim["BasicLeverToggle"].speed = 1;
            anim["BasicLeverToggle"].time = 0;
            anim.Play("BasicLeverToggle");
            activateTargets();
            initialPosition = false;
        }else
        {
            anim["BasicLeverToggle"].speed = -1;
            anim["BasicLeverToggle"].time = anim["BasicLeverToggle"].length;
            anim.Play("BasicLeverToggle");
            deactivateTargets();
            initialPosition = true;
        }

    }
}
