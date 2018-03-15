using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public static UIManager uiManager;

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
       
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
        /*
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
        }*/

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject menu = transform.Find("Main Menu").gameObject;

            bool windowWasOpen = false;

            foreach(Transform t in transform)
            {
                if(t.gameObject.activeSelf == true)
                {
                    windowWasOpen = true;
                    t.gameObject.SetActive(false);
                }
            }

            if (windowWasOpen)
                return;

            menu.gameObject.SetActive(true);
            
            
            
        }
	}



    public void ShowDeathScreen(GameObject killer)
    {
        Debug.Log("SHOWING DEATH SCREEN");
        var ds = transform.Find("DeathScreen").gameObject;
        ds.SetActive(true);
    }

    public void ShowVictoryScreen()
    {
        var vs = transform.Find("VictoryScreen").gameObject;
        vs.SetActive(true);
    }
}
