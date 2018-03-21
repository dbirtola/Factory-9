using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGearSequence : ScriptableSequence {
    public GameObject gear1;
    public GameObject gear2;


    public GameObject smokeParticle;

    public Light[] lightsToBeTurnedOff;
    public float turnOffSpeed = 1f;

    protected override void Start()
    {
        base.Start();

        sequenceName = "EndSequence";
    }

    protected override IEnumerator PlayScriptedSequence()
    {
        factoryCamera.target = gameObject;
        gear1.GetComponent<Rotator>().speed = 0;
        gear2.GetComponent<Rotator>().speed = 0;

        yield return new WaitForSeconds(0.5f);

        var temp = Instantiate(smokeParticle, gear1.transform.position, Quaternion.identity);

        Instantiate(smokeParticle, gear2.transform.position, Quaternion.identity);

        yield return StartCoroutine(turnOffLights());

        //GameManager.gameManager.FadeToBlack();

        yield return null;
    }


    IEnumerator turnOffLights()
    {
       
        foreach(Light l in lightsToBeTurnedOff)
        {
            while(l.intensity > 0)
            {
                l.intensity -= turnOffSpeed * Time.deltaTime;
                yield return null;

            }
        }

        yield return true;
        
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Player>())
        {
            ScriptableSequence.PlaySequenceByName("EndSequence");
        }

    }



}
