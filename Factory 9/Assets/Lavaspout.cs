using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lavaspout : Activateable {


    public float timeOn = -1f;
	// Use this for initialization
	public override void Start () {
        base.Start();
        if(timeOn != -1)
        {
            StartCoroutine(toggleOnOff());
        }

        if (activated)
        {
            transform.GetComponentInChildren<ParticleSystem>().enableEmission = true;
        }else
        {
            transform.GetComponentInChildren<ParticleSystem>().enableEmission = false;
        }
	}
	

    IEnumerator toggleOnOff()
    {
        while (true)
        {
            Activate();
            yield return new WaitForSeconds(timeOn);

            Deactivate();
            yield return new WaitForSeconds(timeOn);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}

    public override void Activate()
    {
        base.Activate();

        transform.GetComponentInChildren<ParticleSystem>().enableEmission = true;

    }
    public override void Deactivate()
    {
        base.Deactivate();

        transform.GetComponentInChildren<ParticleSystem>().enableEmission = false;
    }
}
