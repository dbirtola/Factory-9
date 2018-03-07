using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public static UIManager uiManager;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
       
    }

	// Use this for initialization
	void Start () {
        if(uiManager != null)
        {
            Destroy(uiManager.gameObject);
                return;
        }else
        {
            uiManager = this;
        }
        Init();
	}
	
    public void Init()
    {
        PlayerController.playerController.playerRobot.robotDiedEvent.AddListener(ShowDeathScreen);
        Debug.Log("Set event to event of: " + PlayerController.playerController.playerRobot);
        Debug.Log("Also null? : " + PlayerController.playerController);

    }
	// Update is called once per frame
	void Update () {
		
	}



    public void ShowDeathScreen(GameObject killer)
    {
        var ds = transform.Find("DeathScreen").gameObject;
        ds.SetActive(true);
    }

    public void ShowVictoryScreen()
    {
        var vs = transform.Find("VictoryScreen").gameObject;
        vs.SetActive(true);
    }
}
