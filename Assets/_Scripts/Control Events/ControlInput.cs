using UnityEngine;
using UnityEngine.EventSystems;

public class ControlInput : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public enum ControlSelection
    {
        left, right, brakes, nitro //, forward, backward,
    }

    public ControlSelection currentSelection = ControlSelection.left;

    //public delegate void OnUpArrowPressed();
    //public static OnUpArrowPressed upArrowPressed;

    //public delegate void OnUpArrowReleased();
    //public static OnUpArrowReleased upArrowReleased;


    //public delegate void OnDownButtonPressed();
    //public static OnDownButtonPressed onDownButtonPressed;

    //public delegate void OnDownButtonReleased();
    //public static OnDownButtonReleased onDownButtonReleased;


    public delegate void OnLeftArrowPressed();
    public static OnLeftArrowPressed onLeftButtonPressed;

    public delegate void OnLeftArrowReleased();
    public static OnLeftArrowPressed onLeftButtonReleased;


    public delegate void OnRightButtonPressed();
    public static OnRightButtonPressed onRightButtonPressed;

    public delegate void OnRightButtonReleased();
    public static OnRightButtonReleased onRightButtonReleased;

    public delegate void OnBrakesPressed();
    public static OnBrakesPressed onBrakesPressed;

    public delegate void OnBrakeReleased();
    public static OnBrakeReleased onBrakeReleased;

    public delegate void OnNitroPressed();
    public static OnNitroPressed onNitroPressed;

    public delegate void OnNitroReleased();
    public static OnNitroReleased onNitroReleased;

    public void OnPointerDown(PointerEventData eventData)
    {


        switch(currentSelection)
        {
            case ControlSelection.left:
                if (onLeftButtonPressed != null)
                    onLeftButtonPressed();
                break;

            case ControlSelection.right:
                if (onRightButtonPressed != null)
                    onRightButtonPressed();
                break;

            //case ControlSelection.forward:
            //    if (upArrowPressed != null)
            //        upArrowPressed();
            //    break;

            //case ControlSelection.backward:
            //    if (onDownButtonPressed != null)
            //        onDownButtonPressed();
            //    break;

            case ControlSelection.brakes:
                if (onBrakesPressed != null)
                    onBrakesPressed();
                break;

            case ControlSelection.nitro:
                if (onNitroPressed != null)
                    onNitroPressed();

                // play the particle system
                GetComponentInChildren<ParticleSystem>().Play();

                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (currentSelection)
        {
            case ControlSelection.left:
                if (onLeftButtonReleased != null)
                    onLeftButtonReleased();
                break;

            case ControlSelection.right:
                if (onRightButtonReleased != null)
                    onRightButtonReleased();
                break;

            //case ControlSelection.forward:
            //    if (upArrowReleased != null)
            //        upArrowReleased();
            //    break;

            //case ControlSelection.backward:
            //    if (onDownButtonReleased != null)
            //        onDownButtonReleased();
            //    break;

            case ControlSelection.brakes:
                if (onBrakeReleased != null)
                    onBrakeReleased();
                break;

            case ControlSelection.nitro:
                if (onNitroReleased != null)
                    onNitroReleased();

                // stop the particle system
                GetComponentInChildren<ParticleSystem>().Stop();

                break;

        }
    }

}
