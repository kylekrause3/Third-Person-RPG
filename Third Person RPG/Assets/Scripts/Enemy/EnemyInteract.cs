using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteract : Interactable
{
    //PrimaryAction action = PrimaryAction.Attack;\
    private Defense defense;

    private void Start()
    {
        defense = GetComponent<Defense>();
    }

    public override void Interact()
    {
        this.player.GetComponent<Attack>().startAttack(this.transform);
    }

    public override void OnDefocused()
    {
        
        if(defense.getUpdate() == true)
        {
            defense.onDeDefense();
        }
        this.player.GetComponent<Attack>().endAttack();

        base.OnDefocused();
        /*
            isFocus = false;
            player = null;
            hasInteracted = false;
        */
    }


}
