using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//All scripted sequences should inherit from this class
//Create an empty game object with your script attached to it and set the references you need

//You will not be able to set references to anything outside of your level including the player, UIManager, GameManager etc...
//For those references, you should override the Initialize function and get your references there, likely through
//FindObjectOfType<> or in some cases you may be able to access them via a static variable
//See the IntroSequence script for an example

//Every sequence should set sequenceName in Awake() and override the PlayScriptedSequence() corotuine
//with the logic of there sequence



public class ScriptableSequence : MonoBehaviour {

    //Set this in the Awake() or Start() function of your sequence
    //Will be used to play your sequence from other scripts without getting an explicit reference
    protected string sequenceName;

    protected FactoryCamera factoryCamera;

    protected virtual void Start()
    {
        factoryCamera = FindObjectOfType<FactoryCamera>();
    }

    //Allows you to start the sequence without getting a reference to it from any script.
    //The names are case sensitive
    static public bool PlaySequenceByName(string sequenceName)
    {
        //Search for a sequence we can succesfully play by the matching name
        ScriptableSequence[] sequences = FindObjectsOfType<ScriptableSequence>();
        foreach(ScriptableSequence ss in sequences)
        {
            if(ss.sequenceName == sequenceName)
            {
                if (ss.Play())
                {
                    return true;
                }
            }
        }

        return false;
    }


    //No need to mess with this function, basically just a wrapper for the coroutine
    //but returns false if your initialize function fails
    public bool Play()
    {
        if (!Initialize())
        {
            return false;
        }
        
        StartCoroutine(PlayScriptedSequence());
        return true;
    }


    //This is where the magic happens. Write all your code for the sequence itself in here
    //If you want to remove player control, some functions exist inside the player controller to do so like
    //DisableMovementForDuration and SetMovementEnabled. You can get a reference to the player controller through
    //PlayerController.playerController
    protected virtual IEnumerator PlayScriptedSequence()
    {
        return null;
    }



    //Override this function to get all references you arent able to setup within the scene
    //This could be things like the camera or player, which may reside in other scenes.
    //No need to call base.Initialize(), but if you do make sure you do it at the end of your function

    //Returning false (perhaps because a reference is null at the end) will stop play from running
    //and possibly prevent a crash
    protected virtual bool Initialize()
    {
        return true;
    }

}
