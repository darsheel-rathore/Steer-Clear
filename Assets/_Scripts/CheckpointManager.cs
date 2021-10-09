using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    // creating static instance
    public static CheckpointManager instance;

    [SerializeField] private Transform spawnPosition;

    private Vector3 lastCheckpointPosition;
    private Quaternion lastCheckpointRotation;

    private void Awake()
    {
        if (CheckpointManager.instance == null)
            CheckpointManager.instance = this;

        lastCheckpointPosition = spawnPosition.position;
        lastCheckpointRotation = spawnPosition.rotation;
    }

    private void Start()
    {
    }

    #region Getters

    public Vector3 GetLastCheckPointPosition()
    {
        return lastCheckpointPosition;
    }

    public Quaternion GetLastCheckpointRotation()
    {
        return lastCheckpointRotation;
    }

    #endregion


    #region Setters

    public void SetLastCheckPointPosition(Vector3 lastPosition)
    {
        this.lastCheckpointPosition = lastPosition;
    }

    public void SetLastCheckPointRotation(Quaternion lastRotation)
    {
        this.lastCheckpointRotation = lastRotation;
    }

    #endregion
}
