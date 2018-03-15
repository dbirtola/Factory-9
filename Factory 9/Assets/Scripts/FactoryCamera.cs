using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FactoryCameraMode
{
    Game,
    Cinematic
}

public class FactoryCamera : MonoBehaviour {

    public GameObject target;
    public Vector2 baseOffset;

    public float playerPanningSpeed = 5f;

    public float maxPlayerPanOffset = 10f;

    public float returnSpeed = 5f;

    //For panning side to side
    private Vector2 temporaryOffset;

    bool shouldTrackTarget = true;

    private FactoryCameraMode mode = FactoryCameraMode.Game;


    private float zPosition;

    void Awake()
    {
        zPosition = transform.position.z;
    }


    void Start()
    {
        //DontDestroyOnLoad(gameObject);
    }

    void FixedUpdate()
    {
        if (target == false)
            return;

        if (shouldTrackTarget == false)
            return;

        //Only allow panning in game mode
        if (mode == FactoryCameraMode.Game)
        {
            bool panning = false;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                panning = true;
                if (temporaryOffset.x < maxPlayerPanOffset)
                    temporaryOffset.x += playerPanningSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                panning = true;
                if (temporaryOffset.x > maxPlayerPanOffset * -1)
                    temporaryOffset.x -= playerPanningSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                panning = true;
                if (temporaryOffset.y < maxPlayerPanOffset)
                    temporaryOffset.y += playerPanningSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                panning = true;
                if (temporaryOffset.y > maxPlayerPanOffset * -1)
                    temporaryOffset.y -= playerPanningSpeed * Time.deltaTime;
            }


            if (panning == false)
            {
                float xReturn = (temporaryOffset.x - baseOffset.x) / returnSpeed;
                float yReturn = (temporaryOffset.y - baseOffset.y) / returnSpeed;
                temporaryOffset = temporaryOffset - new Vector2(xReturn, yReturn);
            }

        }else if (mode == FactoryCameraMode.Cinematic)
        {


        }


        /*
        Vector3 newPosition = new Vector3();
        //Set x and y to the targets x and y
        newPosition.x = target.transform.position.x + baseOffset.x + temporaryOffset.x;
        newPosition.y = target.transform.position.y + baseOffset.y + temporaryOffset.y;
        newPosition.z = transform.position.z; //Using the cameras z at all times.
        */



        Vector3 newPosition = new Vector3();
        //We dont want to be adjusting the Z position of our camera, as if it gets too close we may
        //clip through the background
        Vector3 positionWithoutZ = transform.position;
        positionWithoutZ.z = 0;

        //If we are far away from the target we will move toward it frame by frame
        //If not we are within 1 frame of movement away we will simply snap to it
        if((target.transform.position - positionWithoutZ).magnitude >= returnSpeed)
        {
            Vector3 direction = (target.transform.position - positionWithoutZ).normalized;
            newPosition = transform.position + direction * returnSpeed;
        }else
        {
            newPosition = target.transform.position;
            newPosition.z = zPosition;
        }

        newPosition.x += temporaryOffset.x + baseOffset.x;
        newPosition.y += temporaryOffset.y + baseOffset.y;
        transform.position = newPosition;
    }


    public void EnterCinematicMode()
    {
        mode = FactoryCameraMode.Cinematic;
    }

    public void EndCinematicMode()
    {
        mode = FactoryCameraMode.Game;
    }


    
    

   public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
