using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform Follow;
    public float lerpSpeed = 0.125f;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = Follow.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, newPosition, lerpSpeed);
        transform.position = smoothedPosition;
    }
}
