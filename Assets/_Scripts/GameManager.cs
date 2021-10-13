using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for save system
using BayatGames.SaveGameFree;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    #region Variables

    // for static instance
    public static GameManager instance;

    // constants for saving identifiers
    public const string GOLDAMOUNTIDENT = "GoldAmount";
    public const string SELECTEDCAR = "SelectedCar";
    public const string LEVELTIMINGDISPLAY = "LevelTimings";
    public const string LEVELTOTALTIMINGSAVE = "LevelTotalTimings";

    // event delegates
    public delegate void RePositionCarAsPerSave();
    public static RePositionCarAsPerSave loadCarPositionSaveData;

    // car position saved data
    private int selectedCarIndex;

    // level time management data
    private Dictionary<int, string> levelTimings;
    private Dictionary<int, float> totalTimingsForLevels;

    #endregion


    // ==================================================


    #region Unity Methods

    private void OnEnable()
    {
       // SaveGame.Clear();
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }


    private void Awake()
    {
        // setting up singleton
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }
        else
        {
            if (GameManager.instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
    }

    #endregion


    // ==================================================


    #region Private Methods

    private void CheckForSelectedCarSave()
    {
        bool isCarSelectionSaveExists = SaveGame.Exists(SELECTEDCAR);

        if (isCarSelectionSaveExists)
        {
            selectedCarIndex = SaveGame.Load<int>(SELECTEDCAR);

            if (loadCarPositionSaveData != null)
                loadCarPositionSaveData();
        }
        else
        {
            SaveSelectedCar(0);
        }
    }


    private void CheckForLevelStatus()
    {
        bool isLevelDataSaved = SaveGame.Exists(LEVELTIMINGDISPLAY);

        if (!isLevelDataSaved)
        {
            SaveLevelDataForFirstTime();
        }
    }

    #endregion


    // ==================================================


    #region Event Listeners

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        int buildIndex = scene.buildIndex;

        // when the loaded scene is main menu
        if (buildIndex == 0)
        {
            CheckForSelectedCarSave();
            CheckForLevelStatus();
        }
    }

    #endregion


    // ==================================================


    #region Getters

    public int GetSelectedCarSaveData()
    {
        return selectedCarIndex;
    }

    #endregion


    // ==================================================


    #region SAVING AND LOADING METHODS

    public void SaveSelectedCar(int currentlySelectedCar)
    {
        selectedCarIndex = currentlySelectedCar;
        SaveGame.Save<int>(SELECTEDCAR, selectedCarIndex);
    }

    private void SaveLevelDataForFirstTime()
    {
        // initializing level time management variables
        levelTimings = new Dictionary<int, string>();
        totalTimingsForLevels = new Dictionary<int, float>();

        levelTimings.Add(0, "--");
        totalTimingsForLevels.Add(0, 0f);

        SaveGame.Save<Dictionary<int, string>>(LEVELTIMINGDISPLAY, levelTimings);
        SaveGame.Save<Dictionary<int, float>>(LEVELTOTALTIMINGSAVE, totalTimingsForLevels);
    }

    #endregion



}
