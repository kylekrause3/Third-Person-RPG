using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Death : MonoBehaviour //EnemyDeath
{
    public Transform UI_Canvas;

    public virtual void Start()
    {
        
    }
    void Update()
    {
        float health = this.GetComponent<Health>().getHealth();

        if (health <= 0f)
        {
            onDeath();
        }
    }

    public virtual void onDeath()
    {
        if (UI_Canvas == null)
        {
            this.gameObject.SetActive(false);

            return;
        }

        Transform pfDeathScreen = Resources.Load<Transform>("DeathScreen");

        for (int i = 0; i < UI_Canvas.transform.childCount; i++)
        {
            Transform Go = UI_Canvas.transform.GetChild(i);
            
            Go.gameObject.SetActive(false);
        }

        //Transform hitSplatTransform = Instantiate(pfHitSplat, position, Quaternion.identity, parent);
        //Debug.Log(pfDeathScreen);

        
        Transform deathScreen = Instantiate(pfDeathScreen, UI_Canvas);

        this.gameObject.SetActive(false);

    }
}
