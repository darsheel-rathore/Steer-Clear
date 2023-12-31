using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvasManager : MonoBehaviour
{
    [SerializeField] Button leftButton, rightButton;

    [SerializeField] private Sprite soundOn, soundOff;

    [Header("Audio Refs")]
    [SerializeField] private AudioSource btnAudioSource;
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private Image bmgButtonSoundImage, sfxButtonSoundImage;

    [SerializeField] private GameObject loadingPanel;

    public delegate void OnPlayButtonPressed();
    public static OnPlayButtonPressed playButtonPressed;

    private void OnEnable()
    {
        CarShowCaseButtons.leftButtonReleased += CheckForCarShowcaseChange;
        CarShowCaseButtons.rightButtonReleased += CheckForCarShowcaseChange;
        GameManager.loadCarPositionSaveData += LoadSelectedSaveCarData;
    }

    private void OnDisable()
    {
        CarShowCaseButtons.leftButtonReleased -= CheckForCarShowcaseChange;
        CarShowCaseButtons.rightButtonReleased -= CheckForCarShowcaseChange;
        GameManager.loadCarPositionSaveData -= LoadSelectedSaveCarData;
    }

    private void Awake()
    {
        if (btnAudioSource == null)
            btnAudioSource = BtnAudioSource.instance.GetComponent<AudioSource>();

        if (bgmAudioSource == null)
            bgmAudioSource = GameMusicManager.instance.GetComponent<AudioSource>();
    }
    private void Start()
    {
        CheckForCarShowcaseChange();
    }

    private void CheckForCarShowcaseChange()
    {
        int currentSelection = CarCollection.instance.GetActiveCarIndex();
        int totalCars = CarCollection.instance.GetCarList().Count;

        // enable disable left and right button according to current active selection of car
        leftButton.enabled = currentSelection == 0 ? false : true;
        rightButton.enabled = currentSelection == (totalCars - 1) ? false : true;
    }


    // main menu canvas ui callback methods
    #region Main Menu Canvas UI Callback Methods

    // for background music
    public void _OnBGMSoundToggle()
    {
        bgmAudioSource = BtnAudioSource.instance.GetComponent<AudioSource>();
        // getting volume of the audio source 
        float volume = bgmAudioSource.volume;
        
        // if volume is not muted, need to disable the animator and then reduce the volume
        bgmAudioSource.GetComponent<Animator>().enabled = volume > 0f ? false : true;
        bgmAudioSource.volume = volume <= 0f ? 0.5f : 0f;

        // change sprite
        bmgButtonSoundImage.sprite = volume <= 0f ? soundOn : soundOff;
    }


    // for btn sound 
    public void _OnGameSoundToggle()
    {
        float volume = btnAudioSource.volume;

        btnAudioSource.volume = volume <= 0f ? 1f : 0f;
        sfxButtonSoundImage.sprite = volume <= 0f ? soundOn : soundOff;
    }


    // quit game
    public void _OnConfirmGameQuit()
    {
        Application.Quit();
        //System.Diagnostics.Process.GetCurrentProcess().Kill();
    }


    // for firing ad event
    public void _OnConfirmRewardAd()
    {
        // add logic
    }


    public void _PlayButtonPressed()
    {
        if (playButtonPressed != null)
            playButtonPressed();

        loadingPanel.SetActive(true);
        SceneManagement.instance.MainMenuPlayButton();
    }

    #endregion



    #region Event Listeners

    private void LoadSelectedSaveCarData()
    {
        CheckForCarShowcaseChange();
    }

    #endregion

}
