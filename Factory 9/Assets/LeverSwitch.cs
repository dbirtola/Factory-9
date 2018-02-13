using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverSwitch : Switch {




    void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 colNorm = col.contacts[0].normal;
        float AngleOfCol = Vector2.Angle(colNorm, transform.right);
        Debug.Log(AngleOfCol);
    }
}
