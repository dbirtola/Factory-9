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

    public float pushingPower = 0f;

    public const float impactThresholdForDamage = 40;
    public  float invulnerableTimeAfterDamaged = 0.5f;

    void Awake()
    {
        robotDiedEvent = new RobotDiedEvent();
        robotDamagedEvent = new RobotDamagedEvent();
    }

	// Use this for initialization
	void Start () {
        /*
        if (leftArm != null)
            EquipLeftArm(leftArm);
        if (rightArm != null)
            EquipRightArm(rightArm);
        if (legs != null)
            swapLegs(legs);
            */
	}
	
	// Update is called once per frame
	void Update () {
        //Debugging
        if(GetComponent<Player>())
            transform.Find("Body").Find("HeadLamp").gameObject.SetActive(headLampActive);
		
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

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<HingeJoint2D>())
        {
            

            //Only deal damage if player is slower than the other object
            //Player should not take damage for going too fast
            if (col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude >= GetComponent<Rigidbody2D>().velocity.magnitude)
            {
                if(col.contacts.Length <= 0)
                {
                    Debug.Log("Contacts 0");
                    return;
                }

                if(col.contacts[0].normalImpulse >= impactThresholdForDamage)
                {
                    Debug.Log("Took damage from impulse of: " + col.contacts[0].normalImpulse + "(" + col.gameObject + ")");
                  
                    takeDamage(1, col.gameObject);
                }

            }
                //Debug.Log(col.relativeVelocity.magnitude * col.gameObject.GetComponent<Rigidbody2D>().mass);// < impactThresholdForDamage);


            
        }
    }

    public bool addLegs(Legs newLegs)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

            //Prepare body to have legs placed underneath
            transform.position += new Vector3(0, 1);
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            transform.rotation = Quaternion.identity;


        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        newLegs.transform.SetParent(transform.Find("LegsSlot"), false);
        newLegs.transform.localPosition = Vector3.zero;
        newLegs.transform.rotation = Quaternion.identity;
        Destroy(newLegs.GetComponent<Rigidbody2D>());
        //newLegs.GetComponent<Rigidbody2D>().simulated = false;


        jumpPower += newLegs.jumpPowerBoost;
        speed += newLegs.speedBoost;

        legs = newLegs;
        return true;
    }


    public bool swapLegs(Legs newLegs)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (legs == null)
        {
            //Prepare body to have legs placed underneath
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
        //newLegs.GetComponent<Rigidbody2D>().simulated = false;


        LoseLegs(new Vector2(0, 0));

        jumpPower += newLegs.jumpPowerBoost;
        speed += newLegs.speedBoost;

        legs = newLegs;
        return true;
    }

    public LeftArm EquipLeftArm(LeftArm newArm)
    {
        LeftArm temp = leftArm;
        if(leftArm != null)
            pushingPower -= leftArm.pushingPower;

        newArm.transform.SetParent(transform.Find("LeftArmSlot"));
        newArm.transform.localPosition = Vector3.zero;
        newArm.transform.rotation = Quaternion.identity;
        Destroy(newArm.GetComponent<Rigidbody2D>());

        leftArm = newArm;
        pushingPower += newArm.pushingPower;
        return temp;
    }

    public RightArm EquipRightArm(RightArm newArm)
    {
        RightArm temp = rightArm;
        if(rightArm != null)
            pushingPower -= rightArm.pushingPower;

        newArm.transform.SetParent(transform.Find("RightArmSlot"));
        newArm.transform.localPosition = Vector3.zero;
        newArm.transform.rotation = Quaternion.identity;
        Destroy(newArm.GetComponent<Rigidbody2D>());

        rightArm = newArm;
        pushingPower += newArm.pushingPower;

        return temp;
    }


    public void takeDamage(int damage, GameObject attacker)
    {
        if (isInvulnerable == true)
            return;

        robotDamagedEvent.Invoke(attacker);

        //We want the player to first lose his arms, then lose his legs. If he has neither he dies
        for(int i = 0; i < damage; i++)
        {
            if (LoseArm() == false)
            {
                if (LoseLegs(new Vector2(0, 0)) == false)
                {
                    Die();
                }
            }
        }


    }


    public bool LoseArm(bool shouldKnock = false)
    {
        Arm lostArm = null;
        //If we have a left arm we will drop it. If not we drop the right arm. If neither we return false and move on
        if (leftArm != null)
        {
            lostArm = leftArm;
            leftArm = null;

        }
        else if (rightArm != null)
        {
            lostArm = rightArm;
            rightArm = null;
        }else
        {
            return false;
        }
        pushingPower -= lostArm.pushingPower;
        lostArm.transform.SetParent(null);
        //lostArm.GetComponent<Rigidbody2D>().isKinematic = false;
        lostArm.gameObject.AddComponent<Rigidbody2D>();

        lostArm = null;
        return true;
    }

    public bool LoseLegs(Vector2 knockPower)
    {
        if (legs == null)
            return false;

        speed -= legs.speedBoost;
        jumpPower -= legs.jumpPowerBoost;

        legs.transform.SetParent(null);
        var rb = legs.gameObject.AddComponent<Rigidbody2D>();
        rb.mass = 10;
        //legs.GetComponent<Rigidbody2D>().isKinematic = false;
        rb.AddForce(knockPower);
        legs = null;

        //Allow robot core to rotate freely
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

        return true;
    }

    public void Die(GameObject killer = null)
    {
        robotDiedEvent.Invoke(killer);
    }
}
