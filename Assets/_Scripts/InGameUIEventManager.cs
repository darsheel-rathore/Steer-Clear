using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIEventManager : MonoBehaviour
{
    public delegate void OnResetCheckpointButtonPressed();
    public static OnResetCheckpointButtonPressed resetCheckPoint;

    public delegate void OnRestartLevelButtonPressed();
    public static OnRestartLevelButtonPressed restartLevel;

    [SerializeField] private GameObject playerFallCanvas;

    private void OnEnable()
    {
        FallManager.playerFellFromTrack += PlayerFellFromTrack;
    }

    private void OnDisable()
    {
        FallManager.playerFellFromTrack -= PlayerFellFromTrack;
    }

    private void PlayerFellFromTrack()
    {
        playerFallCanvas.SetActive(true);
    }

    public void _OnRestartLevelPanel()
    {
        if (restartLevel != null)
            restartLevel();

        playerFallCanvas.SetActive(false);
    }

    public void _OnResetCheckPointPanel()
    {
        if (resetCheckPoint != null)
            resetCheckPoint();

        playerFallCanvas.SetActive(false);
    }
    
}
