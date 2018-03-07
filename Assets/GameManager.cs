using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class checkPointLevelPair
{
    public string LevelName;
    public Checkpoint checkPoint;
}

public class GameManager : MonoBehaviour {
    public static GameManager gameManager;

    public checkPointLevelPair[] checkPointLevelPairs;

    public Checkpoint activeCheckpoint;

    public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);


        if (gameManager != null)
        {
            Destroy(gameObject);
            return;
        }
        gameManager = gameObject.GetComponent<GameManager>();

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("h"))
        {
            SceneManager.LoadScene("TestSceneAdditive", LoadSceneMode.Additive);
        }
	}


    public void AddScene(string sceneName)
    {
    }

    public void GoToLevel(string level)
    {
        SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
        foreach(checkPointLevelPair checkPointLevelPair in checkPointLevelPairs)
        {
            if (checkPointLevelPair.LevelName == level)
            {
                Debug.Log("Going to level: " + level);
                PlayerController.player.transform.position = checkPointLevelPair.checkPoint.transform.position;
                activeCheckpoint = checkPointLevelPair.checkPoint;
            }
        }
    }

    public void RestartLevel()
    {
        StartCoroutine(restartLevel());
    }

    IEnumerator restartLevel()
    {
        Destroy(PlayerController.player.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //PlayerController.player.transform.position = activeCheckpoint.transform.position;
        yield return null;
        var player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        DontDestroyOnLoad(player.gameObject);
        player.transform.position = activeCheckpoint.transform.position;
        Camera.main.GetComponent<FactoryCamera>().target = player;
        UIManager.uiManager.Init();
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
