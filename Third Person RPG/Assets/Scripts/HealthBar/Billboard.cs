using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate() //always called after regular update
    {
        this.transform.LookAt(transform.position + mainCamera.forward);
    }
}
