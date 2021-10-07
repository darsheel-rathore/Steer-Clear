using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftArrowInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void OnLeftArrowPressed();
    public static OnLeftArrowPressed onLeftButtonPressed;

    public delegate void OnLeftArrowReleased();
    public static OnLeftArrowPressed onLeftButtonReleased;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onLeftButtonPressed != null)
            onLeftButtonPressed();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (onLeftButtonReleased != null)
            onLeftButtonReleased();
    }
}
