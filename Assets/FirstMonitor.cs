using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMonitor : Monitor{

    public GameObject secondaryTextMesh;
    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ShowSecondary();
        }
    }



    public void ShowSecondary()
    {
        textMesh.SetActive(false);
        secondaryTextMesh.SetActive(true);
    }

}
