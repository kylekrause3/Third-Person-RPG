using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CamRotate : MonoBehaviour
{
    public float maxRotation = 90f;
    public float speed = 1f;

    private float og_rotation;
    private float amount_rotated = 0f;
    private void Start()
    {
        og_rotation = transform.eulerAngles.y;
        
    }
    private void Update()
    {
        //transform.rotation = Quaternion.Euler(0f, (maxRotation * o(Time.time * speed) * -1f) + (og_rotation / 2), 0f);
        transform.Rotate(0f, speed, 0f);
        amount_rotated += Math.Abs(speed);
        if(amount_rotated > maxRotation)
        {
            speed *= -1;
            amount_rotated = 0f;
        }
    }

    float f(float x)
    {
        return -1f * Mathf.Cos(x);
    }

    float f_deriv(float x)
    {
        return Mathf.Sin(x);
    }

    float n(float x)
    {
        return 2 * (Mathf.Ceil(x) - 0.5f);
    }

    float r(float x)
    {
        return n(f_deriv(x) / Mathf.PI);
    }

    float g(float x)
    {
        return 2 * ((-1f * r(x)) * Mathf.Floor(x / 2));
    }

    float p(float x)
    {
        return (r(x) * x) - r(x) + g(x);
    }

    float o(float x)
    {
        return -1f * (-2f * (Mathf.Abs(p(x / 2))) + 1);
    }
}
