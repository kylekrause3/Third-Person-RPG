using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTD : MonoBehaviour
{

    public void quit()
    {
        Application.Quit();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quit();
        }
    }
}
