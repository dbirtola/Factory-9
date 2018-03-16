using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHeadLamp : MonoBehaviour {


    public bool headLampOn = false;

    Animator animator;
    public Light headLight;
    public Color defaultColor { get; private set; }
    public Color chaseColor;

    void Awake()
    {
        animator = GetComponent<Animator>();
        headLight = GetComponent<Light>();
        defaultColor = headLight.color;
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

      
    }

    public void TurnOn(bool flicker = false)
    {
        Debug.Log("Turning on");
        headLampOn = true;
        animator.SetBool("On", true);
        if (flicker)
        {
            animator.SetTrigger("Flicker");
        }else
        {
            animator.SetTrigger("QuickFade");
        }

  
    }

    public void SetColor(Color c)
    {
        headLight.color = c;
    }

    public void Flicker()
    {
        animator.SetTrigger("Flicker");
    }

    public void TurnOff(bool flicker = false)
    {
        headLampOn = false;
        animator.SetBool("On", false);
        if (flicker)
        {
            animator.SetTrigger("Flicker");
        }else
        {
            animator.SetTrigger("QuickFade");
        }
    }

    public void FlickerLight(bool on = true)
    {
        if (on)
        {
            GetComponent<Animation>().Play("HeadLampFlicker");

        }
    }
    

    //not used
    IEnumerator Flicker(float pauseTime)
    {

            headLight.intensity = 0f;
            yield return new WaitForSeconds(pauseTime);
            headLight.intensity = 100f;
    }

}
