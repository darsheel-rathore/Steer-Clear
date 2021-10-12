using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIEventManager : MonoBehaviour
{
    public delegate void OnResetCheckpointButtonPressed();
    public static OnResetCheckpointButtonPressed resetCheckPoint;

    public delegate void OnRestartLevelButtonPressed();
    public static OnRestartLevelButtonPressed restartLevel;

    public delegate void OnCarSoundToggle();
    public static OnCarSoundToggle carSoundToggle;

    public delegate void OnBgmSoundToggle();
    public static OnBgmSoundToggle bgmSoundToggle;


    [Header("Panels and Canvas")]
    [SerializeField] private GameObject playerFallCanvas;
    [SerializeField] private GameObject raceFinisedPanel;
    [SerializeField] private float delayTimeForRaceFinishPanel;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject loadingPanel;

    [Header("Car and BGM Audio")]
    [SerializeField] private Image bgmButtonSoundImage;
    [SerializeField] private Image carButtonSoundImage;
    [SerializeField] private Sprite soundOn, soundOff;

    private void OnEnable()
    {
        FallManager.playerFellFromTrack += PlayerFellFromTrack;
        WinningStage.raceFinished += RaceFinished;
    }

    private void OnDisable()
    {
        FallManager.playerFellFromTrack -= PlayerFellFromTrack;
        WinningStage.raceFinished -= RaceFinished;
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
        SceneManagement.instance.ReloadCurrentScene();
    }

    public void _OnResetCheckPointPanel()
    {
        if (resetCheckPoint != null)
            resetCheckPoint();

        playerFallCanvas.SetActive(false);
    }

    public void RaceFinished()
    {
        StartCoroutine(EnableRaceFinishCanvas());
    }

    private IEnumerator EnableRaceFinishCanvas()
    {
        yield return new WaitForSeconds(delayTimeForRaceFinishPanel);
        raceFinisedPanel.SetActive(true);
        timeText.text = "Finished In : " + GameTimer.instance.GetTimerValue();
    }


    // ============================================


    #region Public UI Callbacks
    
    public void _ReloadScene()
    {
        // reload scene
        SceneManagement.instance.ReloadCurrentScene();
    }

    public void _SettingsButtonPressed()
    {
        Time.timeScale = 0f;
        settingsPanel.SetActive(true);
    }

    public void _CarSoundButtonPressed()
    {
        if (carSoundToggle != null)
            carSoundToggle();

        carButtonSoundImage.sprite = carButtonSoundImage.sprite == soundOff ? soundOn : soundOff;
    }
    
    public void _BgSoundPressed()
    {
        if (bgmSoundToggle != null)
            bgmSoundToggle();

        bgmButtonSoundImage.sprite = bgmButtonSoundImage.sprite == soundOff ? soundOn : soundOff;
    }

    public void _OnHomeButtonPressed()
    {
        loadingPanel.SetActive(false);
        // load scene
        SceneManagement.instance.HomeButton();
        // normalizing time if game is paused
        Time.timeScale = Time.timeScale != 1f ? 1f : Time.timeScale;
    }

    public void _NxtLevelButtonPressed()
    {
        // go to next level
        SceneManagement.instance.LoadNextScene();
    }

    #endregion

}
