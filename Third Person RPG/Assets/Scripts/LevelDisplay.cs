using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    private PlayerXP player;
    TextMeshProUGUI levelTMP;

    private void Start()
    {
        levelTMP = this.transform.GetComponentInChildren<TextMeshProUGUI>();

        //levelTMP = this.gameObject.AddComponent<TextMeshProUGUI>();
        player = GameObject.Find("Player").GetComponent<PlayerXP>();
    }

    private void Update()
    {
        levelTMP.text = "Level: " + player.getLevel(XPParent.XPType.Attack);
        //Debug.Log(player.getLevel(XPParent.XPType.Attack));
    }
}
