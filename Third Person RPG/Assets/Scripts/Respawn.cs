using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Transform UI_Canvas;
    private GameObject player;
    private Transform spawn;

    void Awake()
    {
        player = GameObject.Find("Player");
        spawn = GameObject.Find("Map").transform.Find("Spawn");
        UI_Canvas = GameObject.Find("UI").transform;
        Debug.Log(spawn);

    }

    public void respawn()
    {
        player.GetComponent<Health>().setToMaxHealth();
        player.GetComponent<PointAndClick>().RemoveFocus();


        //player.transform.position = spawn.transform.position + new Vector3(0, player.position.y, 0);
        player.SetActive(true);
        spawn.gameObject.SetActive(true);
        player.transform.position = spawn.transform.position;
        spawn.gameObject.SetActive(false);


        for (int i = 0; i < UI_Canvas.transform.childCount; i++)
        {
            Transform Go = UI_Canvas.transform.GetChild(i);

            Go.gameObject.SetActive(true);
        }


        Destroy(this.transform.parent.gameObject);
    }
}
