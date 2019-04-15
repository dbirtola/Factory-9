using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Deprecated in favor of using AirVentEntrance. Originally intended to generate air vents programatically based on nodes placed throughout an area
public class AirVent : MonoBehaviour {
    
    //Width of the generated air vents walls
    public float width = 2f;

    public BoxCollider2D ventWallPrefab;

    public void GenerateCollider()
    {

        Debug.Log(transform.childCount);
        //Do not use transform.childCount in loop, as the number will increase when we create walls
        int childCount = transform.childCount;
        for(int i = 0; i < childCount - 1; i+=2)
        {
            Transform child = transform.GetChild(i);
            Transform child2 = transform.GetChild(i + 1);

            //Create a wall on both sides of the child
            CreateWall(child.position - child.right * width, child2.position - child2.right * width);
            CreateWall(child.position + child.right * width, child2.position + child2.right * width);
        }

    }

    void CreateWall(Vector2 point1, Vector2 point2)// Quaternion rotation)
    {
        //Find the midpoint
        Vector2 position = (point1 + point2) / 2;

        Debug.Log("instantiating");
        BoxCollider2D wall = Instantiate(ventWallPrefab, position, Quaternion.identity);
        wall.transform.up = point2 - point1;
        Debug.Log("parenting");
        wall.transform.SetParent(gameObject.transform, true);

        Vector2 newSize = new Vector2(wall.size.x, Vector2.Distance(point1, point2));
        wall.size = newSize;
        
    }

}
