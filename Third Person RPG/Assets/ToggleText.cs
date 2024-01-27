using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToggleText : MonoBehaviour
{
    public string text_on;
    public string text_off;
    private TextMeshProUGUI UI_Text;

    private bool isOn = false;

    void Start()
    {
        UI_Text = GetComponentInChildren<TextMeshProUGUI>();

    }

    public void OnToggleValueChanged()
    {
        UI_Text.text = isOn ? text_off : text_on;

        isOn = !isOn;

    }
}
