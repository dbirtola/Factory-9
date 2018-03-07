using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryMachine : MonoBehaviour {

    //Factory Machine can take in multiple inputs and dispense multiple outputs
    //Uses an array for input and an array for output
    //Example: input[0] should relate to output[0], input[1] to output[1], and so on.
    public GameObject[] inputObject;
    public GameObject[] outputObject;
    public float buildTime;


    private Vector3 spawnPosition;
    private GameObject input;
    private GameObject output;
    private int counter;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {

        for (int i = 0; i < inputObject.Length; i++)
        {
            if (inputObject[i] == null)
                i++;
            else
                if (coll.gameObject.tag == inputObject[i].gameObject.tag)//delete scrap metal and instantiate Missile
            {
                counter = i;
                coll.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<BoxCollider2D>().isTrigger = false;
                input = coll.gameObject;
                StartCoroutine(BuildTime(buildTime));
            }
        }
    }
    IEnumerator BuildTime(float pauseTime)
    {

        yield return new WaitForSeconds(pauseTime);
        Destroy(input);
        spawnPosition = transform.position;
        if (outputObject.Length > 0)
        {
            output = outputObject[counter].gameObject;
            Instantiate(output, spawnPosition, Quaternion.identity);
        }
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
}
