using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawn : MonoBehaviour
{
    private Transform taggedSpawn;
    private Transform spawn;

    private void Start()
    {
        try
        {
            spawn = GameObject.FindWithTag("Player Spawn").transform;
            GetComponent<Movement>().teleport(spawn.position);
        }
        catch (NullReferenceException)
        {
            Debug.Log("Spawn.cs: No defined player spawn point. (Does player spawn have 'Player Spawn' tag? Does player have a Movement script?)");
        }
    }
}
