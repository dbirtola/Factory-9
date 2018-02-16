using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CauseOfDeath
{
    //Reference to killer object, if responsible
    GameObject killer;
    //Weapon or object which wait might not need this class hold on
    string cause;
}
