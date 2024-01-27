using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerTracker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private PointAndClick player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PointAndClick>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        player?.toggleWalk();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        player?.toggleWalk();
    }
}
