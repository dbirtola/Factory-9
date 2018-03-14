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

    public float hitPauseTime = 0.15f;

    public bool loadMainOnStart = true;

	// Use this for initialization
	void Start () {
        //DontDestroyOnLoad(gameObject);


        if (gameManager != null)
        {
            Destroy(gameObject);
            return;
        }
        gameManager = gameObject.GetComponent<GameManager>();

        if(loadMainOnStart)
            GoToLevel("Main");

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
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
        if(currentScene != SceneManager.GetSceneByName("Init"))
        {
            SceneManager.UnloadSceneAsync(currentScene);
        }
        SceneManager.sceneLoaded += OnLoaded;
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

    static public void HitPause()
    {
        gameManager.StartCoroutine(gameManager.HitPauseRoutine());
    }

    public IEnumerator HitPauseRoutine()
    {
        yield return new WaitForSecondsRealtime(0.035f);
        Time.timeScale = 0;
        Camera.main.orthographicSize -= 0.45f;
        yield return new WaitForSecondsRealtime(hitPauseTime);
        Time.timeScale = 1;
        Camera.main.orthographicSize += 0.45f;
    }

    public void OnLoaded(Scene loaded, LoadSceneMode sceneMode)
    {
        SceneManager.SetActiveScene(loaded);
    }

    public void RestartLevel()
    {
        StartCoroutine(restartLevel());
    }

    IEnumerator restartLevel()
    {
        Destroy(PlayerController.player.gameObject);
        var parts = FindObjectsOfType<BodyPart>();
        foreach(var p in parts)
        {
            Destroy(p.gameObject);
        }
        //SceneManager.LoadScene(SceneManager.GetSceneByName("Init").buildIndex, LoadSceneMode.Single);
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.UnloadSceneAsync(index);
        //SceneManager.LoadScene(SceneManager.GetSceneByName("Init").buildIndex, LoadSceneMode.Single);
        SceneManager.LoadScene(index, LoadSceneMode.Additive);
        //PlayerController.player.transform.position = activeCheckpoint.transform.position;
        //Alow the scene to load
        yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(index));
        var player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        //var player = FindObjectOfType<Player>();
        //DontDestroyOnLoad(player.gameObject);
        player.transform.position = activeCheckpoint.transform.position;
        Camera.main.GetComponent<FactoryCamera>().target = player.gameObject;
        UIManager.uiManager.Init();
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
