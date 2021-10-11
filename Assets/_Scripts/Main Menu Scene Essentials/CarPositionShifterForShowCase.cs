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
    }

    private void OnDisable()
    {
        CarShowCaseButtons.leftButtonReleased -= ShiftLeft;
        CarShowCaseButtons.rightButtonReleased -= ShiftRight;
    }

    // Update is called once per frame
    void Update()
    {
        //if (startShifting)
        //{
        //    transform.position = Vector3.Slerp(transform.position, targetPosition, shiftSpeed * Time.deltaTime);

        //    if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
        //    {
        //        transform.position = targetPosition;
        //        startShifting = false;
        //    }
        //}
    }

    private void ShiftLeft()
    {
        if (!readyToShift) return;

        positionDifference = -1f * leftSidePositionDifference;
        targetPosition = new Vector3(positionDifference, 0f, 0f) + transform.position;
        StartCoroutine(LerpPosition(targetPosition, 0.5f));
    }

    private void ShiftRight()
    {
        if (!readyToShift) return;

        positionDifference = rightSidePositionDifference;
        targetPosition = new Vector3(positionDifference, 0f, 0f) + transform.position;
        StartCoroutine(LerpPosition(targetPosition, 0.5f));
    }

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
