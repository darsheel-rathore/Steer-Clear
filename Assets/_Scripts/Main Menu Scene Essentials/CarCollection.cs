using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollection : MonoBehaviour
{
    #region Vars

    // for static instance
    public static CarCollection instance;

    // list of cars in showcase collection
    private List<GameObject> carList;

    // currently active car index
    private int currentlySelectedCar = 0;

    // caching first / any car in the child
    private CarPositionShifterForShowCase car;

    #endregion


    // ===========================================


    #region Unity Methods

    private void OnEnable()
    {
        CarShowCaseButtons.leftButtonReleased += CarChangeLeftButtonPressed;
        CarShowCaseButtons.rightButtonReleased += CarChangeRightButtonPressed;
    }

    private void OnDisable()
    {
        CarShowCaseButtons.leftButtonReleased -= CarChangeLeftButtonPressed;
        CarShowCaseButtons.rightButtonReleased -= CarChangeRightButtonPressed;
    }

    private void Awake()
    {
        // creating static instance
        if (CarCollection.instance == null)
            CarCollection.instance = this;

        // caching first car
        car = GetComponentInChildren<CarPositionShifterForShowCase>();
    }

    void Start()
    {
        PoplulateCarlist();
    }

    #endregion


    // =========================================


    #region Private Methods

    private void PoplulateCarlist()
    {
        // initialize and populate car list
        carList = new List<GameObject>();
        foreach (Transform t in transform)
        {
            carList.Add(t.gameObject);
        }
    }

    #endregion


    // =========================================


    #region Event Listeners

    private void CarChangeRightButtonPressed()
    {
        if (car.GetReadyToShiftStatus())
            currentlySelectedCar += 1;
    }

    private void CarChangeLeftButtonPressed()
    {
        if (car.GetReadyToShiftStatus())
            currentlySelectedCar -= 1;
    }


    #endregion


    // =========================================


    #region Getters

    // Getter for carList
    public List<GameObject> GetCarList()
    {
        return carList;
    }

    public int GetActiveCarIndex()
    {
        return currentlySelectedCar;
    }

    #endregion
}
