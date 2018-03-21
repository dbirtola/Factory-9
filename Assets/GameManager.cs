using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public Scene sceneToBeUnloaded;

    public Image fadeToBlackImage;
    public float fadeSpeed = 0.5f;


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




        //SceneManager.sceneLoaded += OnLoaded;
    }
	
	// Update is called once per frame
	void Update () {

	}


    public void AddScene(string sceneName)
    {
    }

    public void GoToLevel(string level)
    {
        StartCoroutine(gotoLevel(level));
        /*
        ///Scene currentScene = SceneManager.GetActiveScene();

        if(SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Init"))
        {
            //SceneManager.UnloadSceneAsync(currentScene);
            sceneToBeUnloaded = SceneManager.GetActiveScene();
        }
        SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);

        //SceneManager.sceneLoaded += OnLoaded;
        */
    }

    public void StartGame()
    {
        StartCoroutine(startGame());
    }
    public IEnumerator startGame()
    {
        fadeToBlackImage.gameObject.SetActive(true);

        yield return StartCoroutine(loadLevel("Main", true));
        Checkpoint mainCheckPoint = null;
        foreach(checkPointLevelPair cp in checkPointLevelPairs)
        {
            if (cp.LevelName == "Main")
                mainCheckPoint = cp.checkPoint;
        }

        PlayerController.player.transform.position = mainCheckPoint.transform.position;
        activeCheckpoint = mainCheckPoint;

        Vector3 newCameraPosition = PlayerController.player.transform.position;
        newCameraPosition.z = Camera.main.transform.position.z;
        Camera.main.transform.position = newCameraPosition;
        Camera.main.GetComponent<FactoryCamera>().target = PlayerController.player.gameObject;

        yield return StartCoroutine(fadeOut(fadeToBlackImage));
        fadeToBlackImage.gameObject.SetActive(false);

    }


    public void FadeToBlack()
    {
        fadeToBlackImage.gameObject.SetActive(true);
        StartCoroutine(fadeOut(fadeToBlackImage, true));
    }

    public IEnumerator fadeOut(Image img, bool reverse = false)
    {
        if(reverse == false)
        {
            while (img.color.a > 0)
            {
                Color c = img.color;
                c.a -= fadeSpeed * Time.deltaTime;
                img.color = c;
                yield return null;
            }

        }else
        {

            while (img.color.a < 1)
            {
                Color c = img.color;
                c.a += fadeSpeed * Time.deltaTime;
                img.color = c;
                yield return null;
            }
        }

        yield return true;
    }

    public IEnumerator gotoLevel(string level)
    {
        PlayerController.playerController.playerRobot.stripRobot();
        var parts = FindObjectsOfType<BodyPart>();
        foreach(BodyPart part in parts)
        {
            Destroy(part.gameObject);
        }
        //Put up splash screen

        yield return StartCoroutine(loadLevel(level, true));

        //Move player
        foreach (checkPointLevelPair checkPointLevelPair in checkPointLevelPairs)
        {
            if (checkPointLevelPair.LevelName == level)
            {
                PlayerController.player.transform.position = checkPointLevelPair.checkPoint.transform.position;
                activeCheckpoint = checkPointLevelPair.checkPoint;

                Vector3 newCameraPosition = PlayerController.player.transform.position;
                newCameraPosition.z = Camera.main.transform.position.z;
                Camera.main.transform.position = newCameraPosition;
                Camera.main.GetComponent<FactoryCamera>().target = PlayerController.player.gameObject;

            }
        }

        PlayerController.playerController.SetMovementEnabled(true);
        //Take down splash screen


    }


    public void RestartFromCheckpoint()
    {
        StartCoroutine(restartFromCheckpoint());

      
    }

    IEnumerator restartFromCheckpoint()
    {

        PlayerController.playerController.playerRobot.stripRobot() ;
        PlayerController.player.gameObject.SetActive(false);
        yield return StartCoroutine(loadLevel(activeCheckpoint.sceneName, true));
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Init"));

        if (PlayerController.player != null)
        {
            Destroy(PlayerController.player.gameObject);
            yield return null;
        }
        var player = Instantiate(playerPrefab, transform.position, Quaternion.identity);

        // GameObject player = PlayerController.player.gameObject;
        player.transform.position = activeCheckpoint.transform.position;
        PlayerController.player.gameObject.SetActive(true);
        Camera.main.GetComponent<FactoryCamera>().target = player.gameObject;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(activeCheckpoint.sceneName));
        UIManager.uiManager.Init();
        PlayerController.playerController.SetMovementEnabled(true);

    }
    /*
    public void ResetToCheckpoint(bool unloadOthers = true)
    {
        StartCoroutine(resetToCheckpoint(activeCheckpoint, unloadOthers));
    }
    */

    public void loadLevelAsync(string level)
    {
        StartCoroutine(loadLevel(level, false));
    }
    public IEnumerator loadLevel(string levelName, bool unloadOthers = true)
    {
        
        Debug.Log("Loading scnee: " + levelName);
        AsyncOperation asyncOp;

        var parts = FindObjectsOfType<BodyPart>();
        foreach (BodyPart part in parts)
        {
            Destroy(part.gameObject);
        }
        List<Scene> scenesToBeUnloaded = new List<Scene>();
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Init"));
        if (unloadOthers == true)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name != "Init")
                {
                    scenesToBeUnloaded.Add(SceneManager.GetSceneAt(i));
                    //asyncOp = SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
                    //yield return asyncOp;
                }
            }
        }

        foreach(Scene s in scenesToBeUnloaded)
        {
            asyncOp = SceneManager.UnloadSceneAsync(s);
            yield return asyncOp;
        }

        asyncOp = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        yield return asyncOp;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelName));
        PlayerController.playerController.SetMovementEnabled(true);
        yield return true;

    }


    /*
    IEnumerator resetToCheckpoint(Checkpoint checkpoint, bool unloadOthers = true)
    {
        AsyncOperation asyncOp;

        var parts = FindObjectsOfType<BodyPart>();
        foreach (var part in parts)
        {
            Destroy(part.gameObject);
        }

        
        if (unloadOthers == true)
        {
            for(int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name != "Init")
                {

                    asyncOp = SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
                    yield return asyncOp;
                }
            }
        }
        

        asyncOp = SceneManager.LoadSceneAsync(checkpoint.sceneName, LoadSceneMode.Additive);
        yield return asyncOp;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(checkpoint.sceneName));

        Destroy(PlayerController.player.gameObject);
        var p = Instantiate(playerPrefab, checkpoint.transform.position, Quaternion.identity);
        Camera.main.GetComponent<FactoryCamera>().target = p.gameObject;
        UIManager.uiManager.Init();



    }
   */

    /*
    public IEnumerator StartLevel(string levelName, Checkpoint checkpoint)
    {
        Scene previousScene = SceneManager.GetActiveScene();

        //Splash to cover loading goes here

        //Unload our last scene unless it was Init
        if (previousScene != SceneManager.GetSceneByName("Init"))
        {
            SceneManager.UnloadSceneAsync(previousScene);
        }else
        {

        }

        SceneManager.LoadSceneAsync(SceneManager.GetSceneByName(levelName).buildIndex, LoadSceneMode.Additive);

    }


    public void OnFinishedUnloading(Scene scene)
    {
    }

    public void OnFinishedLoading(Scene scene)
    {

    }

    */


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



    public void CloseGame()
    {
        Application.Quit();
    }
}
