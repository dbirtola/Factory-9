using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    Rigidbody2D rb;
    Player player;

    public bool canJump = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }


	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.D))
        {
            MoveHorizontal(player.speed * Time.deltaTime);

        }else if (Input.GetKey(KeyCode.A))
        {
            MoveHorizontal(-1 * player.speed * Time.deltaTime);
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(canJump == true)
            {
                Jump();
            }
        }
	}


    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Entered");
        if (col.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            canJump = true;
        }
    }



    void MoveHorizontal(float speed)
    {
        //transform.position += Vector3.right * speed;
        rb.AddForce(Vector2.right * speed);
    }


    void Jump()
    {
        canJump = false;
        rb.AddForce(Vector2.up * player.jumpPower);
    }




}
