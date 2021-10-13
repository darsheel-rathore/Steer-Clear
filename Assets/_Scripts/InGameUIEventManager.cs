using BayatGames.SaveGameFree;
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
    [SerializeField] private TextMeshProUGUI bestTimeText;

    [Header("Car and BGM Audio")]
    [SerializeField] private Image bgmButtonSoundImage;
    [SerializeField] private Image carButtonSoundImage;
    [SerializeField] private Sprite soundOn, soundOff;


    // timer values
    private string currentTime;
    private string bestTime;


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
        SaveGameTime();
    }

    private IEnumerator EnableRaceFinishCanvas()
    {
        yield return new WaitForSeconds(delayTimeForRaceFinishPanel);
        raceFinisedPanel.SetActive(true);
        timeText.text = currentTime;
        bestTimeText.text = bestTime;
    }

    private void SaveGameTime()
    {
        // getting saved data for times
        Dictionary<int, string> timeDisplay = GameManager.instance.GetLevelTiming();
        Dictionary<int, float> timeTotal = GameManager.instance.GetLevelTotalTiming();


        // getting scene index and total time in completion
        int currentSceneIndex = SceneManagement.instance.GetCurrentSceneIndex();
        float tTimeInCompletion = GameTimer.instance.GetTotalTimerValue();
        string currentTimeDisplay = GameTimer.instance.GetTimerValue();

        // we know when player plays for the first time, the 
        // total time in timeTotal will be 0f
        if (timeTotal[currentSceneIndex-1] == 0f)
        {
            // hence player clearing it for the first time
            int currentLevelsUnlockedCount = SaveGame.Load<int>("UnlockedLevelCount");
            currentLevelsUnlockedCount += 1;
            SaveGame.Save<int>("UnlockedLevelCount", currentLevelsUnlockedCount);

            // save another level default values 
            Dictionary<int, string> t1 = SaveGame.Load<Dictionary<int, string>>("LevelTimings");
            Dictionary<int, float> t2 = SaveGame.Load<Dictionary<int, float>>("LevelTotalTimings");

            t1.Add(currentLevelsUnlockedCount-1, "--");
            t2.Add(currentLevelsUnlockedCount-1, 0f);

            SaveGame.Save<Dictionary<int, string>>("LevelTimings", t1);
            SaveGame.Save<Dictionary<int, float>>("LevelTotalTimings", t2);
        }

        // check for broken record
        bool isRecordBroken = ((tTimeInCompletion < timeTotal[currentSceneIndex-1]) && 
            (timeTotal[currentSceneIndex-1] != 0f));

        // when record is broken
        if (isRecordBroken)
        {
            // overwriting dictionaries
            timeDisplay[currentSceneIndex-1] = currentTimeDisplay;
            timeTotal[currentSceneIndex-1] = tTimeInCompletion;

            // saving new data
            SaveGame.Save<Dictionary<int, string>>("LevelTimings", timeDisplay);
            SaveGame.Save<Dictionary<int, float>>("LevelTotalTimings", timeTotal);

            // updating text components for display
            currentTime = "Finished In : " + currentTimeDisplay.ToString();
            bestTime = "Best Time : " + currentTimeDisplay.ToString();
        }
        // when record is not broken
        else
        {
            // incase of first attempt
            if (timeTotal[currentSceneIndex - 1] == 0f)
            {
                // save another level default values 
                Dictionary<int, string> t1 = SaveGame.Load<Dictionary<int, string>>("LevelTimings");
                Dictionary<int, float> t2 = SaveGame.Load<Dictionary<int, float>>("LevelTotalTimings");

                t1[currentSceneIndex - 1] = currentTimeDisplay;
                t2[currentSceneIndex - 1] = tTimeInCompletion;

                SaveGame.Save<Dictionary<int, string>>("LevelTimings", t1);
                SaveGame.Save<Dictionary<int, float>>("LevelTotalTimings", t2);
            }

            // updating text components for display
            currentTime = "Finished In : " + currentTimeDisplay;
            bestTime = "Best Time : " + timeDisplay[currentSceneIndex-1];
        }
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
