using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameLimit : MonoBehaviour
{
    public int limit = 60;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = limit;
    }
}
