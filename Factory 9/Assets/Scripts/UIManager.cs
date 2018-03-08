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

    }
	// Update is called once per frame
	void Update () {
        if (transform.Find("Levels") == null)
            return;
        if (Input.GetKey(KeyCode.Tab))
        {
            transform.Find("Levels").gameObject.SetActive(true);
        }else
        {
            transform.Find("Levels").gameObject.SetActive(false);

        }

        if (Input.GetKey(KeyCode.C))
        {
            transform.Find("Controls").gameObject.SetActive(true);
        }else
        {
            transform.Find("Controls").gameObject.SetActive(false);
        }
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
