using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpArrowInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void OnUpArrowPressed();
    public static OnUpArrowPressed upArrowPressed;

    public delegate void OnUpArrowReleased();
    public static OnUpArrowReleased upArrowReleased;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (upArrowPressed != null)
            upArrowPressed();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (upArrowReleased != null)
            upArrowReleased();
    }
}
