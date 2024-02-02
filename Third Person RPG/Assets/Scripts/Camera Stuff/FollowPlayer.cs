using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // NOTE: This class should not really be used, as CamOrbit.cs now follows the followtarget/player anyway
    public Transform Follow;
    public float lerpSpeed = 0.125f;
    public bool doLerp = true;

    // Update is called once per frame
    void Update()
    {
        if (doLerp)
        {
            Lerp();
        }
        else
        {
            transform.position = Follow.position;
        }
    }

    public void Lerp()
    {
        Vector3 newPosition = Follow.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, newPosition, lerpSpeed);
        transform.position = smoothedPosition;
    }
}
