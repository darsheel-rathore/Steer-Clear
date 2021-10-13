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
    public const string UNLOCKEDLEVEL = "UnlockedLevelCount";

    // event delegates
    public delegate void RePositionCarAsPerSave();
    public static RePositionCarAsPerSave loadCarPositionSaveData;

    // car position saved data
    private int selectedCarIndex;
    private int unlockedLevels;

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
        WinningStage.raceFinished += LevelFinished;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
        WinningStage.raceFinished -= LevelFinished;
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

    private void LevelDataInitializer()
    {
        // initializing level time management variables
        levelTimings = new Dictionary<int, string>();
        totalTimingsForLevels = new Dictionary<int, float>();
    }

    private void LevelDataPopulators()
    {
        levelTimings = SaveGame.Load<Dictionary<int, string>>(LEVELTIMINGDISPLAY);
        totalTimingsForLevels = SaveGame.Load<Dictionary<int, float>>(LEVELTOTALTIMINGSAVE);
    }

    private void CheckForUnlockedLevelCount()
    {
        bool anyLevelUnlocked = SaveGame.Exists(UNLOCKEDLEVEL);

        if (!anyLevelUnlocked)
        {
            unlockedLevels = 1;
            SaveGame.Save<int>(UNLOCKEDLEVEL, unlockedLevels);
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
            CheckForUnlockedLevelCount();
        }
    }

    private void LevelFinished()
    {
        LevelDataInitializer();
        LevelDataPopulators();
    }

    #endregion


    // ==================================================


    #region Getters

    public int GetSelectedCarSaveData()
    {
        return selectedCarIndex;
    }

    public Dictionary<int, string> GetLevelTiming()
    {
        return levelTimings;
    }

    public Dictionary<int, float> GetLevelTotalTiming()
    {
        return totalTimingsForLevels;
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
        // initialize dictionaries
        LevelDataInitializer();

        levelTimings.Add(0, "--");
        totalTimingsForLevels.Add(0, 0f);

        SaveGame.Save<Dictionary<int, string>>(LEVELTIMINGDISPLAY, levelTimings);
        SaveGame.Save<Dictionary<int, float>>(LEVELTOTALTIMINGSAVE, totalTimingsForLevels);
    }

    private void NewLevelUnLocked()
    {

    }

    #endregion



}
