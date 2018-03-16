using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    //Set to negative to rotate the other direction
    public float speed = 20f;

    public bool randomBetweenTwoValues;
    public Vector2 values;

    void Start()
    {
        if(randomBetweenTwoValues == true)
        {
            speed = Random.Range(values.x, values.y);
        }
    }
	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(new Vector3(0, 0, speed) * Time.deltaTime);
	}
}
