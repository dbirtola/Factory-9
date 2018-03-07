using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour {

    public bool crushWhenRobotEnters;

    public GameObject crushedCoreObject;

    public float cooldown = 3f;
    
    private float timeLastCrushed;


    public void Crush()
    {
        if (Time.time - timeLastCrushed >= cooldown)
            StartCoroutine(MoveCrusherHead(-50000));
    }

    //These are shorted by the OnCollision trigger
    IEnumerator MoveCrusherHead(float force)
    {
        while (true)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * force);
            yield return null;
        }
    }

    IEnumerator ReturnCrusherHead(float speed)
    {
        while (true)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            yield return null;
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.GetComponent<Robot>())
        {
            //Cant hit a stealthed player
            if (col.gameObject.GetComponent<PlayerController>())
            {
                if (col.gameObject.GetComponent<PlayerController>().isStealthed == true)
                    return;
            }
        
            timeLastCrushed = Time.time;
            Debug.Log("Smashed: " + col.gameObject);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            col.gameObject.GetComponent<Robot>().takeDamage(5, gameObject);
            //Spaghetti?
            if(col.gameObject.transform.Find("Body") != null)
                Destroy(col.gameObject.transform.Find("Body").gameObject);
            var temp = Instantiate(crushedCoreObject, col.gameObject.transform.position, Quaternion.identity);
            temp.transform.SetParent(col.gameObject.transform);
            temp.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 500);
        }


        StopAllCoroutines();
        if(col.gameObject != transform.parent.gameObject)
        {
            Debug.Log(col.gameObject + "CRUSHER");
            StartCoroutine(ReturnCrusherHead(5));
        }



    }


}
