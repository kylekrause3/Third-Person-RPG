using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Don't go through terrain. 

public class CamOrbit : MonoBehaviour
{
    public Transform Player;
    public float distance;
    public bool pncEnabled = true;

    [Header("SmoothDamp Settings")]
    public float smoothTime = 0.075f;
    private Vector3 smoothVelocity = Vector3.zero;

    [Header("PNC Settings")]
    public float StartingAngle = 0;
    public float viewportToRotationRatio; // DEGREES PER VIEWPORT UNIT

    [Header("WASD Settings")]
    public float sensitivity = 100;
    public bool startLocked = false;
    public Transform customFollowTarget;

    private bool toggleCursorOff;
    private bool dorotation;


    private delegate void OrbitBehavior();
    private OrbitBehavior orbit_delegate;

    Camera cam;

    Vector3 prevMousePos;
    Vector3 viewportDelta;

    private Vector3 previousCamPosition;

    private Vector2 rotation;
    private float TrackedXRotation;
    private float TrackedYRotation; // only used for wasd
    private float MinXRotation = -89.99f;
    private float MaxXRotation = 89.99f;
    
    void Start()
    {
        cam = Camera.main;

        if (pncEnabled)
        {
            orbit_delegate = PNCOrbit;
            TrackedXRotation = StartingAngle;
            cam.transform.Rotate(new Vector3(1, 0, 0), StartingAngle);
            cam.transform.position = Player.position;
            previousCamPosition = Player.position;
        }
        else
        {
            if (customFollowTarget == null)
            {
                customFollowTarget = Player;
            }
            orbit_delegate = WASDOrbit;
            TrackedXRotation = 0;
            TrackedYRotation = 0;
            cam.transform.position = customFollowTarget.position;
            previousCamPosition = customFollowTarget.position;
        }
        cam.transform.Translate(new Vector3(0, 0, distance * -1f));

        if (startLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            dorotation = true;
            toggleCursorOff = true;
        }
    }

    void Update()
    {
        orbit_delegate();
    }


    private void PNCOrbit()
    {
        cam.transform.position = Vector3.SmoothDamp(previousCamPosition, Player.position, ref smoothVelocity, smoothTime); ;

        if (Input.GetMouseButton(1))
        {
            viewportDelta = prevMousePos - cam.ScreenToViewportPoint(Input.mousePosition);

            rotation.x = viewportDelta.y * viewportToRotationRatio;//pos is mouse down, neg is mouse up (cam up, cam down)
            rotation.y = viewportDelta.x * viewportToRotationRatio;//pos is mouse right, neg is mouse left (cam right, cam left)
            TrackedXRotation += rotation.x;
            if (TrackedXRotation > MaxXRotation)
            {
                rotation.x = MaxXRotation - (TrackedXRotation - rotation.x); //this is the amount we went over the max
                TrackedXRotation = MaxXRotation;
            }
            else if (TrackedXRotation < MinXRotation)
            {
                rotation.x = MinXRotation - (TrackedXRotation - rotation.x); //this is the amount we went over the min
                TrackedXRotation = MinXRotation;
            }
            cam.transform.Rotate(new Vector3(1, 0, 0), rotation.x); //up and down
            cam.transform.Rotate(new Vector3(0, -1, 0), rotation.y, Space.World); //left and right

        }
        float appliedDistance = distance;
        LayerMask mask = 1 << 6; // to do more layers, you would do 1 << 6 | 1 << <num> | ...;
        // raycast from player to camera to see if there is anything in the way
        RaycastHit hit;
        if (Physics.SphereCast(Player.position, 0.5f, cam.transform.TransformDirection(Vector3.back), out hit, distance, mask))
        {
            // if there is something in the way, move the camera to the hit point
            appliedDistance = Vector3.Distance(hit.point, Player.position);
        }
        previousCamPosition = cam.transform.position;
        cam.transform.Translate(new Vector3(0, 0, appliedDistance * -1f));

        prevMousePos = cam.ScreenToViewportPoint(Input.mousePosition);
    }


    private void WASDOrbit()
    {

        cam.transform.position = Vector3.SmoothDamp(previousCamPosition, customFollowTarget.position, ref smoothVelocity, smoothTime);

        if (Input.GetKeyDown(KeyCode.C))
        {
            toggleCursorOff = !toggleCursorOff;
            if (toggleCursorOff)
            {
                Cursor.lockState = CursorLockMode.Locked;
                dorotation = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                dorotation = false;
            }
        }

        rotation.y = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;//pos is mouse right, neg is mouse left (cam right, cam left)
        rotation.x = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime * -1f;//pos is mouse down, neg is mouse up (cam up, cam down) 

        TrackedXRotation += rotation.x;
        TrackedYRotation += rotation.y;
        if (TrackedXRotation > MaxXRotation)
        {
            rotation.x = MaxXRotation - (TrackedXRotation - rotation.x); //this is the amount we went over the max
            TrackedXRotation = MaxXRotation;
        }
        else if (TrackedXRotation < MinXRotation)
        {
            rotation.x = MinXRotation - (TrackedXRotation - rotation.x); //this is the amount we went over the min
            TrackedXRotation = MinXRotation;
        }

        if(dorotation)
        {
            cam.transform.Rotate(new Vector3(1, 0, 0), rotation.x); //up and down
            cam.transform.Rotate(new Vector3(0, 1, 0), rotation.y, Space.World); //left and right
            Player.transform.Rotate(new Vector3(0, 1, 0), rotation.y, Space.World); //left and right
        }

        float appliedDistance = distance;
        LayerMask mask = 1 << 6; // to do more layers, you would do 1 << 6 | 1 << <num> | ...;
        // raycast from player to camera to see if there is anything in the way
        RaycastHit hit;
        if (Physics.SphereCast(customFollowTarget.position, 0.5f, cam.transform.TransformDirection(Vector3.back), out hit, distance, mask))
        {
            // if there is something in the way, move the camera to the hit point
            appliedDistance = Vector3.Distance(hit.point, customFollowTarget.position);
        }
        previousCamPosition = cam.transform.position;
        cam.transform.Translate(new Vector3(0, 0, appliedDistance * -1f));
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
