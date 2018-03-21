using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour {

    public string sceneName;

	// Use this for initialization
	void Start () {
       // DontDestroyOnLoad(transform.parent.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Player>())
        {
            GameManager.gameManager.activeCheckpoint = this;
        }
    }


}
