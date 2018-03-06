using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadingBounds : MonoBehaviour {

    public string SceneToBeLoaded;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Player>())
            SceneManager.LoadScene(SceneToBeLoaded, LoadSceneMode.Additive);
    }
}
