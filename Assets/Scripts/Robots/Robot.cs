﻿using System.Collections;
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

    public RobotHeadLamp headLamp;

    public float speed = 100;
    public float jumpPower = 200;

    private float pushingPower = 0f;

    public const float impactThresholdForDamage = 90;
    public  float invulnerableTimeAfterDamaged = 0.5f;

    public bool isInAirVent = false;

    GameObject hitParticleEffect;

    void Awake()
    {
        robotDiedEvent = new RobotDiedEvent();
        robotDamagedEvent = new RobotDamagedEvent();
        hitParticleEffect = Resources.Load("take_Damage") as GameObject;
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
        if (GetComponent<Player>())
        {
            if (transform.Find("Body") == null)
                return;
            if (transform.Find("Body").Find("HeadLamp") == null)
                return;
            //if(transform.Find("Body").Find("HeadLamp").gameObject != null)
                //transform.Find("Body").Find("HeadLamp").gameObject.SetActive(headLampActive);
        }
		
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



        //Only deal damage if player is slower than the other object
        //Player should not take damage for going too fast
        if (!col.gameObject.GetComponent<Rigidbody2D>())
            return;


        float sum = 0;
        foreach (ContactPoint2D cp in col.contacts)
        {
            sum += cp.normalImpulse;
        }


        //if (col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude >= GetComponent<Rigidbody2D>().velocity.magnitude)

        // 

        if (!col.gameObject.GetComponent<VelocityTracker>() || !GetComponent<VelocityTracker>())
        {
            return;
        }
        
       if(col.gameObject.GetComponent<VelocityTracker>().velocity.magnitude >= GetComponent<VelocityTracker>().velocity.magnitude)
        {
            //col.contacts[0].

           if (col.contacts.Length <= 0)
            {
                return;
            }


           // if(col.contacts[0].normalImpulse >= impactThresholdForDamage)
           if(sum >= impactThresholdForDamage)
            {

                //Debug.Log(gameObject + " lost with : " + GetComponent<VelocityTracker>().velocity.magnitude + " vs " + col.gameObject.GetComponent<VelocityTracker>().velocity.magnitude);
                //Debug.Log(gameObject + " had " + gameObject.GetComponent<Rigidbody2D>().velocity + " vs " + col.gameObject.GetComponent<Rigidbody2D>().velocity);
                takeDamage(1, col.gameObject);

                if (col.gameObject.GetComponent<Destructable>())
                {
                    Destroy(col.gameObject);
                }
            }

        }
            //Debug.Log(col.relativeVelocity.magnitude * col.gameObject.GetComponent<Rigidbody2D>().mass);// < impactThresholdForDamage);


            
        
    }

    public int getNumberOfParts()
    {
        int numParts = 0;
        if (legs != null)
            numParts++;
        if (rightArm != null)
            numParts++;
        if (leftArm != null)
            numParts++;

        return numParts;
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

        LoseLegs(false);

        jumpPower += newLegs.jumpPowerBoost;
        speed += newLegs.speedBoost;

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        newLegs.transform.SetParent(transform.Find("LegsSlot"), false);
        newLegs.transform.localPosition = Vector3.zero;
        newLegs.transform.localScale = Vector3.one;
        newLegs.transform.rotation = Quaternion.identity;
        Destroy(newLegs.GetComponent<Rigidbody2D>());
        //newLegs.GetComponent<Rigidbody2D>().simulated = false;




        legs = newLegs;
        return true;
    }

    public LeftArm EquipLeftArm(LeftArm newArm)
    {
        LeftArm temp = leftArm;
        LoseArm(temp);

        newArm.transform.SetParent(transform.Find("LeftArmSlot"));
        newArm.transform.localPosition = Vector3.zero;
        newArm.transform.rotation = Quaternion.identity;
        newArm.transform.localScale = Vector3.one;
        Destroy(newArm.GetComponent<Rigidbody2D>());

        leftArm = newArm;
        newArm.owner = gameObject;
        return temp;
    }

    public RightArm EquipRightArm(RightArm newArm)
    {
        RightArm temp = rightArm;
        LoseArm(temp);

        newArm.transform.SetParent(transform.Find("RightArmSlot"));
        newArm.transform.localPosition = Vector3.zero;
        newArm.transform.rotation = Quaternion.identity;
        newArm.transform.localScale = Vector3.one;
        Destroy(newArm.GetComponent<Rigidbody2D>());

        rightArm = newArm;
        newArm.owner = gameObject;

        return temp;
    }

    IEnumerator addForceNextFrame(GameObject go, Vector2 force)
    {
        yield return null;
        if(go != null & go.GetComponent<Rigidbody2D>() != null)
            go.GetComponent<Rigidbody2D>().AddForce(force);

    }

    public void stripRobot()
    {
        for (int i = 0; i < 5; i++)
        {
            if (LoseArm(leftArm, true) == false)
            {
                if (LoseArm(rightArm, true) == false)
                {
                    if (LoseLegs(true) == false)
                    {
                        //Die();
                    }

                }
            }
        }
    }

    public void takeDamage(int damage, GameObject attacker, bool withHitPause = true)
    {
        if (isInvulnerable == true)
            return;

        robotDamagedEvent.Invoke(attacker);

        if(headLamp != null && headLamp.headLampOn == true)
        {
            headLamp.TurnOn(true);
        }

        var particle = Instantiate(hitParticleEffect, transform.position, Quaternion.identity);
        Destroy(particle, 1);

        if (legs != null && withHitPause == true)
            GameManager.HitPause();


        //We want the player to first lose his arms, then lose his legs. If he has neither he dies
        for(int i = 0; i < damage; i++)
        {
            if (LoseArm(leftArm, true) == false)
            {
                if(LoseArm(rightArm, true) == false)
                {
                    if (LoseLegs(true) == false)
                    {
                        Die();
                    }

                }
            }
        }


    }

  


    public bool LoseArm(Arm arm, bool shouldKnock = false)
    {
        if (arm == null)
            return false;
        Arm lostArm = arm;
        //If we have a left arm we will drop it. If not we drop the right arm. If neither we return false and move on
        /*
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
        */
        if (arm == leftArm)
            leftArm = null;
        if (arm == rightArm)
            rightArm = null;


        lostArm.transform.SetParent(null);
        //lostArm.GetComponent<Rigidbody2D>().isKinematic = false;
        lostArm.gameObject.AddComponent<Rigidbody2D>();
        lostArm.transform.position += transform.right * transform.lossyScale.x;
        lostArm.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
        StartCoroutine(addForceNextFrame(lostArm.gameObject, transform.right * transform.lossyScale.x * 200));
        lostArm.owner = null;
        lostArm = null;
        return true;
    }
    Vector2 GetRandomVector(float x, float y)
    {
        return new Vector2(Random.Range(-1 * x, x), Random.Range(-1 * y, y));
    }
    
    public bool LoseLegs(bool shouldKNock)
    {
        if (legs == null)
            return false;

        
        speed -= legs.speedBoost;
        jumpPower -= legs.jumpPowerBoost;

        legs.transform.SetParent(null);
        var rb = legs.gameObject.AddComponent<Rigidbody2D>();
        legs.transform.position += transform.up * -0.15f;
        legs.transform.position += transform.right * transform.lossyScale.x * 0.3f;
        legs.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
        StartCoroutine(addForceNextFrame(legs.gameObject, transform.right * transform.lossyScale.x * 800));


        rb.mass = 10;
        //legs.GetComponent<Rigidbody2D>().isKinematic = false;
        //rb.AddForce(GetRandomVector(100, 100);
        StartCoroutine(addForceNextFrame(legs.gameObject, GetRandomVector(1000, 1000)));
        legs = null;

        //Allow robot core to rotate freely
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

        return true;
    }

    public void Die(GameObject killer = null)
    {
        robotDiedEvent.Invoke(killer);
        if (GetComponent<PlayerController>())
        {
            GetComponent<PlayerController>().SetMovementEnabled(false);
        }
    }
}
