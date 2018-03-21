using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSequence : ScriptableSequence {

    public PlayerController playerController;
    public UIManager uiManager;

    public Robot robotToBeDestroyed;

    void Awake()
    {
        sequenceName = "IntroSequence";
    }





    protected override IEnumerator PlayScriptedSequence()
    {
        playerController.DisableMovementForDuration(2);
        yield return new WaitForSeconds(2);
        Destroy(robotToBeDestroyed.gameObject);

        yield return base.PlayScriptedSequence();
    }





    protected override bool Initialize()
    {
        playerController = PlayerController.playerController;
        uiManager = UIManager.uiManager;
        


        if(playerController == null)
        {
            Debug.LogWarning("PlayerController is null for IntroSequence");
            return false;
        }

        if(uiManager == null)
        {
            Debug.LogWarning("UIManager is null for IntroSequence");
            return false;
        }


        return base.Initialize();
    }

}
