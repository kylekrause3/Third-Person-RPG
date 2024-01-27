using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : Death
{
    private Defense defense;
    private Transform attacker;

    public override void Start()
    {
        base.Start();

        defense = GetComponent<Defense>();
    }
    public override void onDeath()
    {
        
        /* 
            this.gameObject.SetActive(false);
        */
        attacker = defense.getAttacker();
        attacker?.GetComponent<PlayerXP>().addXP(XPParent.XPType.Attack, this.GetComponentInParent<Drop_XP>().getXP());

        base.onDeath();
    }
}
