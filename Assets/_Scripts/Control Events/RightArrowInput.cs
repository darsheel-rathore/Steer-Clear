using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightArrowInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void OnRightButtonPressed();
    public static OnRightButtonPressed onRightButtonPressed;

    public delegate void OnRightButtonReleased();
    public static OnRightButtonReleased onRightButtonReleased;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onRightButtonPressed != null)
            onRightButtonPressed();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (onRightButtonReleased != null)
            onRightButtonReleased();
    }
}
