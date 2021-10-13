using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPositionShifterForShowCase : MonoBehaviour
{
    [SerializeField] private float leftSidePositionDifference;
    [SerializeField] private float rightSidePositionDifference;

    [SerializeField] private bool readyToShift = true;
    [SerializeField] private float shiftSpeed;

    private float positionDifference;
    private Vector3 targetPosition;

    private void OnEnable()
    {
        CarShowCaseButtons.leftButtonReleased += ShiftLeft;
        CarShowCaseButtons.rightButtonReleased += ShiftRight;
        GameManager.loadCarPositionSaveData += LoadSelectedSaveCarData;
    }

    private void OnDisable()
    {
        CarShowCaseButtons.leftButtonReleased -= ShiftLeft;
        CarShowCaseButtons.rightButtonReleased -= ShiftRight;
        GameManager.loadCarPositionSaveData -= LoadSelectedSaveCarData;
    }

    private void Start()
    {
        readyToShift = true;
    }


    #region Event Listeners

    // for shifting car left when left UI button pressed
    private void ShiftLeft()
    {
        if (!readyToShift) return;
        
        positionDifference = -1f * leftSidePositionDifference;
        targetPosition = new Vector3(positionDifference, 0f, 0f) + transform.position;
        StartCoroutine(LerpPosition(targetPosition, 0.5f));
    }


    // for shifting car right when right UI button pressed
    private void ShiftRight()
    {
        if (!readyToShift) return;

        positionDifference = rightSidePositionDifference;
        targetPosition = new Vector3(positionDifference, 0f, 0f) + transform.position;
        StartCoroutine(LerpPosition(targetPosition, 0.5f));
    }


    private void LoadSelectedSaveCarData()
    {
        int positionIndex = GameManager.instance.GetSelectedCarSaveData();
        Vector3 newPosition = transform.position + new Vector3(rightSidePositionDifference * positionIndex, 0, 0);
        transform.position = newPosition;
    }

    #endregion

    // for lerping the car position from current to target
    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        readyToShift = false;

        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        readyToShift = true;
    }


    // getter for ready to shift
    public bool GetReadyToShiftStatus()
    {
        return readyToShift;
    }



}
