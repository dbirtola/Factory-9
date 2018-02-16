using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerController.playerController.playerRobot.robotDiedEvent.AddListener(ShowDeathScreen);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowDeathScreen(GameObject killer)
    {
        var ds = transform.Find("DeathScreen").gameObject;
        ds.SetActive(true);
    }
}
