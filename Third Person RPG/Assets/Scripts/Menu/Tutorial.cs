using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private PointAndClick player;
    public bool showTutorial = true;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PointAndClick>();
        if (showTutorial)
        {
            Transform pfTutorial = Resources.Load<Transform>("Tutorial Screen");
            Transform deathScreen = Instantiate(pfTutorial, this.transform);
            player?.toggleWalk(false);
        }
    }
}
