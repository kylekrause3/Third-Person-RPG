using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAwake : MonoBehaviour
{
    void Awake()
    {
        GameObject.Destroy(gameObject);
    }
}
