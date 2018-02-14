using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {
    public static PlayerController playerController;
    public static Player player;
    Robot playerRobot;
    RobotController playerRC;
    Rigidbody2D rb;

    static public Vector2 GetMouseInWorldSpace()
    {
        Vector2 pos;
        var temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.x = temp.x;
        pos.y = temp.y;
        return pos;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        playerRobot = GetComponent<Robot>();
        playerRC = GetComponent<RobotController>();
    }

    void Start()
    {
        if(playerController != null)
        {
            Destroy(gameObject);
        }else
        {
            playerController = this;
        }

        player = GetComponent<Player>();
    }


	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.D))
        {
            playerRC.MoveHorizontal(playerRobot.speed * Time.deltaTime);

        }else if (Input.GetKey(KeyCode.A))
        {
            playerRC.MoveHorizontal(-1 * playerRobot.speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            playerRobot.headLampActive = !playerRobot.headLampActive;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerRC.FireRightArm();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRC.Jump();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            //For testing
            player.GetComponent<Robot>().takeDamage();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector2 pos = new Vector2(transform.position.x + 1, transform.position.y);
            var colls = Physics2D.OverlapBoxAll(pos, new Vector2(1, 1), 0);

            foreach(Collider2D col in colls)
            {
                /*
                if (col.gameObject.GetComponent<BodyPartItem>())
                {
                    Debug.Log("Equipping: " + col.gameObject.GetComponent<BodyPartItem>().bodyPart);
                    PlayerController.player.GetComponent<Robot>().EquipBodyPart(col.gameObject.GetComponent<BodyPartItem>().bodyPart);
                    //Destroy(col.gameObject);
                }*/
                if (col.gameObject.GetComponent<BodyPart>())
                {
                    Debug.Log("Equipping: " + col.gameObject);
                    PlayerController.player.GetComponent<Robot>().EquipBodyPart(col.gameObject.GetComponent<BodyPart>());
                    //Destroy(col.gameObject);
                }
            }
        }
	}







    void OnTriggerEnter2D(Collider2D col)
    {

    }

    void resetPose(float xOffset = 0, float yOffset = 0)
    {
        transform.position += new Vector3(xOffset, yOffset);
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        transform.rotation = Quaternion.identity;
    }




}
