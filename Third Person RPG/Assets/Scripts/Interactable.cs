using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour //EnemyInteract
{
    public enum PrimaryAction
    {
        WalkTo,
        Attack
    }
    public PrimaryAction primaryAction = PrimaryAction.WalkTo;
    public float radius = 3f;
    public Transform interactionTransform;



    bool isFocus = false;
    [HideInInspector]
    public Transform player;

    bool hasInteracted = false;

    public virtual void Interact() //virtual means we can override it with each type of interactable
    {
        //this method is meant to be overwritten
        Debug.Log("Interacting with " + transform.name);
    }

    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if(distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public virtual void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }*/
}
