using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryCamera : MonoBehaviour {
    public GameObject target;
    public Vector2 baseOffset;

    public float playerPanningSpeed = 5f;

    public float maxPlayerPanOffset = 10f;

    public float returnSpeed = 5f;
    //For panning side to side
    private Vector2 temporaryOffset;

    bool shouldTrackTarget = true;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void FixedUpdate()
    {
        if (target == false)
            return;

        if (shouldTrackTarget == false)
            return;


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


        if(panning == false){
            float xReturn = (temporaryOffset.x - baseOffset.x) / returnSpeed;
            float yReturn = (temporaryOffset.y - baseOffset.y) / returnSpeed;
            temporaryOffset = temporaryOffset - new Vector2(xReturn, yReturn);
        }

        Vector3 newPosition = new Vector3();
        //Set x and y to the targets x and y
        newPosition.x = target.transform.position.x + baseOffset.x + temporaryOffset.x;
        newPosition.y = target.transform.position.y + baseOffset.y + temporaryOffset.y;
        newPosition.z = transform.position.z; //Using the cameras z at all times.

        transform.position = newPosition;
    }




   public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
