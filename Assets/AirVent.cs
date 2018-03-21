using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirVent : MonoBehaviour {

    LineRenderer lineRenderer;
    public float width = 2f;

    public BoxCollider2D ventWallPrefab;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        /*
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);

        foreach (Vector3 pos in positions)
        {
            Debug.Log(pos);
        }*/

        
    }
	

    public void GenerateCollider()
    {

        Debug.Log(transform.childCount);
        //Do not use transform.childCount in loop, as the number will increase when we create walls
        int childCount = transform.childCount;
        for(int i = 0; i < childCount - 1; i++)
        {
            Transform child = transform.GetChild(i);
            Transform child2 = transform.GetChild(i + 1);

            CreateWall(child.position - child.right * width, child2.position - child2.right * width);// child.rotation);
            CreateWall(child.position + child.right * width, child2.position + child2.right * width);// child.rotation);
        }

        /*
        
        Vector2[] points = new Vector2[transform.childCount * 2];
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            Vector2 pos = child.localPosition - child.right * width;

            points[i*2] = pos;

            pos = child.localPosition - child.right * width * 1.1f;

            points[i*2 + 1] = pos;
            
        }
        polyCollider.points = points;
        */
    }

    void CreateWall(Vector2 point1, Vector2 point2)// Quaternion rotation)
    {
        //Find the midpoint
        Vector2 position = (point1 + point2) / 2;
       // Quaternion rotation = Quaternion.LookRotation(point2 - point1);
       // Vector3 euler = rotation.eulerAngles;

        Debug.Log("instantiating");
        BoxCollider2D wall = Instantiate(ventWallPrefab, position, Quaternion.identity);
        wall.transform.up = point2 - point1;
        //wall.transform.eulerAngles = new Vector3(0, 0, euler.z);
        Debug.Log("parenting");
        wall.transform.SetParent(gameObject.transform, true);

        Vector2 newSize = new Vector2(wall.size.x, Vector2.Distance(point1, point2));
        wall.size = newSize;
        
    }

	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {

    }


}
