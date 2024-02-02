using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    private Movement playerMovement;
    public void d()
    {
        playerMovement = GameObject.Find("Player").GetComponent<Movement>();
        try
        {
            playerMovement.toggleWalk(true);
        }
        catch ( NullReferenceException e)
        {
            Debug.Log("Player not found: " + e);
        }
        Destroy(transform.parent.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            d();
        }
    }
}
