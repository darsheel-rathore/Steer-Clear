using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{

    public Text timerText;
    float startTime = 0f;
    string timerValue;
    float seconds = 0, minutes = 0;
    bool isTimerRunning = true;


    private void OnEnable()
    {
        FinishLine.raceFinished += TimerStopped;
        FallManager.playerFellFromTrack += TimerStopped;
        InGameUIEventManager.resetCheckPoint += TimerResumed;
        InGameUIEventManager.restartLevel += ResetTimer;
    }

    private void OnDisable()
    {
        FinishLine.raceFinished -= TimerStopped;
        FallManager.playerFellFromTrack -= TimerStopped;
        InGameUIEventManager.resetCheckPoint -= TimerResumed;
        InGameUIEventManager.restartLevel -= ResetTimer;
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

    private void MaintainGameTimer()
    {
        if (isTimerRunning)
        {
            startTime += Time.deltaTime;
            seconds = startTime;
        }

        if (seconds >= 60)
        {
            minutes += 1;
            seconds = 0f;
            startTime = 0f;
        }

        timerValue = minutes <= 0 ? seconds.ToString("F2") : minutes + ":" + seconds.ToString("F2");

        timerText.text = timerValue;
    }

    private void TimerStopped()
    {
        isTimerRunning = false;
    }

    private void TimerResumed()
    {
        isTimerRunning = true;
    }

    private void ResetTimer()
    {
        isTimerRunning = true;
        startTime = 0f;
        minutes = 0f;
        seconds = 0f;
    }
}
