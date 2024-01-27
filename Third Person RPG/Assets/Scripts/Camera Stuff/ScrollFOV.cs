using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollFOV : MonoBehaviour
{
    public Vector2 FovBoundaries;
    public Vector2 DistanceBoundaries;
    public float FOV_Sensitivity = 2;
    public float Distance_Sensitivity = 0.5f;
    public bool FOVEnabled = true;

    private Camera cam;
    private CamOrbit camOrbit;

    private delegate void OnScroll(float scrollwheel);
    private OnScroll scroll_delegate;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        camOrbit = GetComponent<CamOrbit>();

        if(FOVEnabled)
        {
            scroll_delegate = FOV_Scroll;
        }
        else
        {
            scroll_delegate = Distance_Scroll;
        }
    }
    private void Update()
    {
        float scrollwheel = Input.mouseScrollDelta.y;

        if(scrollwheel != 0)
        {
            scroll_delegate(scrollwheel);
        }
    }

    private void FOV_Scroll(float scrollwheel)
    {
        if(scrollwheel == 1)
        {
            if (cam.fieldOfView > FovBoundaries.x)
            {
                cam.fieldOfView -= FOV_Sensitivity;
            }
        }
        else if(scrollwheel == -1)
        {
            if (cam.fieldOfView < FovBoundaries.y)
            {
                cam.fieldOfView += FOV_Sensitivity;
            }
        }
    }

    private void Distance_Scroll(float scrollwheel)
    {
        if (scrollwheel == 1)
        {
            float camOrbitDistance = camOrbit.getDistance();
            if (camOrbitDistance - Distance_Sensitivity > DistanceBoundaries.x)
            {
                camOrbit.setDistance(camOrbitDistance - Distance_Sensitivity);
            }
            else
            {
                camOrbit.setDistance(DistanceBoundaries.x);
            }
        }
        else if (scrollwheel == -1)
        {
            float camOrbitDistance = camOrbit.getDistance();
            if (camOrbitDistance + Distance_Sensitivity < DistanceBoundaries.y)
            {
                camOrbit.setDistance(camOrbitDistance + Distance_Sensitivity);
            }
            else
            {
                camOrbit.setDistance(DistanceBoundaries.y);
            }
        }
    }


    public void toggleFOV(bool toggle)
    {
        FOVEnabled = toggle;
        if (FOVEnabled)
        {
            scroll_delegate = FOV_Scroll;
        }
        else
        {
            scroll_delegate = Distance_Scroll;
        }
    }
}
