using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DownArrowInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum ControlType
    {
        left, right, forward, backward
    }

    public ControlType dropdown = ControlType.left;

    public delegate void OnDownButtonPressed();
    public static OnDownButtonPressed onDownButtonPressed;

    public delegate void OnDownButtonReleased();
    public static OnDownButtonReleased onDownButtonReleased;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onDownButtonPressed != null)
            onDownButtonPressed();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (onDownButtonReleased != null)
            onDownButtonReleased();
    }
}
