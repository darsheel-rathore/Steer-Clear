using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    // for static reference
    public static GameTimer instance;

    [SerializeField] private TextMeshProUGUI timerText;
    float startTime = 0f;
    string timerValue;
    float seconds = 0, minutes = 0;
    bool isTimerRunning = true;



    #region Unity Methods

    // Event Subscription
    private void OnEnable()
    {
        WinningStage.raceFinished += TimerStopped;
        FallManager.playerFellFromTrack += TimerStopped;
        InGameUIEventManager.resetCheckPoint += TimerResumed;
        InGameUIEventManager.restartLevel += ResetTimer;
    }

    // Event unsubcription
    private void OnDisable()
    {
        WinningStage.raceFinished -= TimerStopped;
        FallManager.playerFellFromTrack -= TimerStopped;
        InGameUIEventManager.resetCheckPoint -= TimerResumed;
        InGameUIEventManager.restartLevel -= ResetTimer;
    }

    // awake
    private void Awake()
    {
        // getting static reference
        if (GameTimer.instance == null)
            GameTimer.instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MaintainGameTimer();
    }

    #endregion


    private void MaintainGameTimer()
    {
        if (isTimerRunning)
        {
            startTime += Time.deltaTime;
            seconds = startTime;
        }

        // update timer values for seconds and minutes
        if (seconds >= 60)
        {
            minutes += 1;
            seconds = 0f;
            startTime = 0f;
        }

        // create display value for timer
        timerValue = minutes <= 0 ? seconds.ToString("F2") : minutes + ":" + seconds.ToString("F2");

        // display value
        timerText.text = timerValue;
    }

    // for stopping timer
    private void TimerStopped()
    {
        isTimerRunning = false;
    }

    // resuming timer
    private void TimerResumed()
    {
        isTimerRunning = true;
    }

    // resetting timer
    private void ResetTimer()
    {
        isTimerRunning = true;
        startTime = 0f;
        minutes = 0f;
        seconds = 0f;
    }

    // getter for timer value in string 
    public string GetTimerValue()
    {
        return timerValue;
    }
}
