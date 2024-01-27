using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Don't go through terrain. 

public class CamOrbit : MonoBehaviour
{
    public Transform Player;
    public float distance;
    public float StartingAngle;

    Camera cam;

    Vector3 prevMousePos;
    Vector3 viewportDelta;

    public float viewportToRotationRatio; // DEGREES PER VIEWPORT UNIT

    private Vector3 oldPosition;

    private Vector2 rotation;
    private float TrackedXRotation;
    public float MinXRotation = -89.99f;
    public float MaxXRotation = 89.99f;
    
    void Start()
    {
        cam = Camera.main;
        cam.transform.position = Player.position;
        cam.transform.Rotate(new Vector3(1, 0, 0), StartingAngle);
        TrackedXRotation = StartingAngle;
        cam.transform.Translate(new Vector3(0, 0, distance * -1f));
    }

    void Update()
    {
        cam.transform.position = Player.position;

        if (Input.GetMouseButtonDown(1))
        {
            prevMousePos = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(1))
        {
            viewportDelta = prevMousePos - cam.ScreenToViewportPoint(Input.mousePosition);

            rotation.x = viewportDelta.y * viewportToRotationRatio;//pos is mouse down, neg is mouse up (cam up, cam down)
            rotation.y = viewportDelta.x * viewportToRotationRatio;//pos is mouse right, neg is mouse left (cam right, cam left)
            if (rotation.x > 0 && TrackedXRotation < MaxXRotation)
            {
                if (rotation.x + TrackedXRotation > MaxXRotation)
                {
                    rotation.x = MaxXRotation - TrackedXRotation;
                }
                TrackedXRotation += rotation.x;
            }
            else if (rotation.x < 0 && TrackedXRotation >= MinXRotation)
            {
                if (rotation.x + TrackedXRotation < MinXRotation)
                {
                    rotation.x = MinXRotation - TrackedXRotation;
                }
                TrackedXRotation += rotation.x;
            }
            cam.transform.Rotate(new Vector3(1, 0, 0), rotation.x); //up and down
            cam.transform.Rotate(new Vector3(0,-1,0), rotation.y, Space.World); //left and right
            
        }
        float appliedDistance = distance;
        // layermask for layer 6 (map)
        int mask = 1 << 6;
        // raycast from player to camera to see if there is anything in the way
        RaycastHit hit;
        if (Physics.SphereCast(Player.position, 0.5f, cam.transform.TransformDirection(Vector3.back), out hit, distance, mask))
        {
            // if there is something in the way, move the camera to the hit point
            appliedDistance = Vector3.Distance(hit.point, Player.position);
        }
        cam.transform.Translate(new Vector3(0, 0, appliedDistance * -1f));

        prevMousePos = cam.ScreenToViewportPoint(Input.mousePosition);
    }

    public void setDistance(float newDistance)
    {
        distance = newDistance;
    }

    public float getDistance()
    {
        return distance;
    }
}
