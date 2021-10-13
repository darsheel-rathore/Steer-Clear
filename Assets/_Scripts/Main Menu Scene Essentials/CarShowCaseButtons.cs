using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CarShowCaseButtons : MonoBehaviour, IPointerUpHandler
{
    
    public enum SelectionDirection {  Left, Right };
    public SelectionDirection currentSelection;

    public delegate void OnLeftCarChangeReleased();
    public static OnLeftCarChangeReleased leftButtonReleased;

    public delegate void OnRightCarChangeReleased();
    public static OnRightCarChangeReleased rightButtonReleased;

    public void OnPointerUp(PointerEventData eventData)
    {
        switch(currentSelection)
        {
            case SelectionDirection.Left:
                if (leftButtonReleased != null)
                    leftButtonReleased();
                break;

            case SelectionDirection.Right:
                if (rightButtonReleased != null)
                    rightButtonReleased();
                break;
        }
    }
}
