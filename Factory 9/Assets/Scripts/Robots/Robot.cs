using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class RobotDiedEvent : UnityEvent<GameObject>
{

}

public class RobotDamagedEvent : UnityEvent<GameObject>
{

}



public class Robot : MonoBehaviour {

    public RobotDiedEvent robotDiedEvent;
    public RobotDamagedEvent robotDamagedEvent;

    public Legs legs;
    public LeftArm leftArm;
    public RightArm rightArm;
    public bool headLampActive = false;
    public bool isInvulnerable = false;

    public float speed = 100;
    public float jumpPower = 200;


    void Awake()
    {
        robotDiedEvent = new RobotDiedEvent();
        robotDamagedEvent = new RobotDamagedEvent();
    }

	// Use this for initialization
	void Start () {
        if (leftArm != null)
            EquipLeftArm(leftArm);
        if (rightArm != null)
            EquipRightArm(rightArm);
        if (legs != null)
            swapLegs(legs);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Find("HeadLamp").gameObject.SetActive(headLampActive);
		
	}



    public bool EquipBodyPart(BodyPart bodyPart)
    {
       

        BodyPart oldPart = null;

        if (bodyPart.GetComponent<Legs>())
        {
            swapLegs(bodyPart.GetComponent<Legs>());
            return true;
        }
        if (bodyPart.GetComponent<LeftArm>())
        {
            oldPart = EquipLeftArm(bodyPart.GetComponent<LeftArm>());
            return true;
        }

        if (bodyPart.GetComponent<RightArm>())
        {
            oldPart =  EquipRightArm(bodyPart.GetComponent<RightArm>());
            return true;
        }

        if(oldPart != null)
        {
            //oldPart.transform.SetParent(null);
            //oldPart.AddComponent<rigid
        }
        return false;
    }

    
    public bool swapLegs(Legs newLegs)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (legs == null)
        {
            transform.position += new Vector3(0, 1);
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            transform.rotation = Quaternion.identity;
        }

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        newLegs.transform.SetParent(transform.Find("LegsSlot"), false);
        newLegs.transform.localPosition = Vector3.zero;
        newLegs.transform.rotation = Quaternion.identity;
        Destroy(newLegs.GetComponent<Rigidbody2D>());


        if (legs != null)
        {
            jumpPower -= legs.jumpPowerBoost;
            speed -= legs.speedBoost;
        }
        jumpPower += newLegs.jumpPowerBoost;
        speed += newLegs.speedBoost;

        legs = newLegs;
        return true;
    }

    public LeftArm EquipLeftArm(LeftArm newArm)
    {
        LeftArm temp = leftArm;

        newArm.transform.SetParent(transform.Find("LeftArmSlot"));
        newArm.transform.localPosition = Vector3.zero;
        newArm.transform.rotation = Quaternion.identity;
        Destroy(newArm.GetComponent<Rigidbody2D>());

        leftArm = newArm;
        return temp;
    }

    public RightArm EquipRightArm(RightArm newArm)
    {
        RightArm temp = rightArm;

        newArm.transform.SetParent(transform.Find("RightArmSlot"));
        newArm.transform.localPosition = Vector3.zero;
        newArm.transform.rotation = Quaternion.identity;
        Destroy(newArm.GetComponent<Rigidbody2D>());

        rightArm = newArm;

        return temp;
    }


    public void takeDamage(GameObject attacker)
    {
        if (isInvulnerable == true)
            return;

        robotDamagedEvent.Invoke(attacker);

        if(LoseArm() == false)
        {
            
        }

    }


    public bool LoseArm(bool shouldKnock = false)
    {
        Arm lostArm = null;
        if (leftArm != null)
        {
            lostArm = leftArm;

        }
        else if (rightArm != null)
        {
            lostArm = rightArm;
        }else
        {
            return false;
        }

        lostArm.transform.SetParent(null);
        lostArm.GetComponent<Rigidbody2D>().isKinematic = false;
        lostArm = null;
        return true;
    }


    public void Die(GameObject killer = null)
    {
        robotDiedEvent.Invoke(killer);
    }
}
