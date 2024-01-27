using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    private PointAndClick player;
    public void d()
    {
        player = GameObject.Find("Player").GetComponent<PointAndClick>();
        player.toggleWalk(true);
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
