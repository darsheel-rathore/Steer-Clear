using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnAndPositionManager : MonoBehaviour
{
    // caching player car body reference
    public GameObject playerVehicleChassis;
    private GameObject playerInstance;

    private Vector3 lastPosition;
    private Quaternion lastRotation;

    private void OnEnable()
    {
        InGameUIEventManager.resetCheckPoint += ResetCheckPoint;
        InGameUIEventManager.restartLevel += RestartLevel;
    }

    private void OnDisable()
    {
        InGameUIEventManager.resetCheckPoint -= ResetCheckPoint;
        InGameUIEventManager.restartLevel -= RestartLevel;
    }

    private void Start()
    {
        InstantiatePlayerToLastLocation();
    }

    private void InstantiatePlayerToLastLocation()
    {
        // get spawn position and rotation
        GetLastRotationAndPosition();

        // instantiate player and provide reference to the virtual camera
        playerInstance = Instantiate(playerVehicleChassis, lastPosition, lastRotation);
        VCHandler.instace.SetVCFollowTarget(playerInstance.GetComponentInChildren<TCCABody>().transform);
    }

    private void ResetCheckPoint()
    {
        InstantiatePlayerToLastLocation();
    }

    private void RestartLevel()
    {
        // reload scene
        SceneManager.LoadScene(0);
    }

    private void GetLastRotationAndPosition()
    {
        lastPosition = CheckpointManager.instance.GetLastCheckPointPosition();
        lastRotation = CheckpointManager.instance.GetLastCheckpointRotation();
    }
}
