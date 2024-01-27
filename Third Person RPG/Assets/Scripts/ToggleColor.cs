using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleColor : MonoBehaviour
{
    public Color color_on;
    public Color color_off;
    private UnityEngine.UI.Image image ;

    private bool isOn = false;
 
    void Start()
    {
        image = GetComponent<UnityEngine.UI.Image>();

    }
 
    public void OnToggleValueChanged()
    {
        image.color = isOn ? color_off : color_on ;

        isOn = !isOn;

    }

}
