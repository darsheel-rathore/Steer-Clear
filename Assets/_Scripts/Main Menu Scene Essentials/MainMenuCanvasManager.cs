using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvasManager : MonoBehaviour
{
    [SerializeField] Button leftButton, rightButton;

    private void OnEnable()
    {
        CarShowCaseButtons.leftButtonReleased += CheckForCarShowcaseChange;
        CarShowCaseButtons.rightButtonReleased += CheckForCarShowcaseChange;
    }

    private void OnDisable()
    {
        CarShowCaseButtons.leftButtonReleased -= CheckForCarShowcaseChange;
        CarShowCaseButtons.rightButtonReleased -= CheckForCarShowcaseChange;
    }


    private void Start()
    {
        CheckForCarShowcaseChange();
    }

    private void CheckForCarShowcaseChange()
    {
        int currentSelection = CarCollection.instance.GetActiveCarIndex();
        int totalCars = CarCollection.instance.GetCarList().Count;

        // enable disable left and right button according to current active selection of car
        leftButton.enabled = currentSelection == 0 ? false : true;
        rightButton.enabled = currentSelection == (totalCars - 1) ? false : true;

    }
}
