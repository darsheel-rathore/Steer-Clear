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
    private const string GOLDAMOUNTIDENT = "GoldAmount";
    private const string SELECTEDCAR = "SelectedCar";

    // event delegates
    public delegate void RePositionCarAsPerSave();
    public static RePositionCarAsPerSave loadCarPositionSaveData;

    // car position saved data
    private int selectedCarIndex = 0;

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
            Debug.Log("No Car Selection Data available.");
            return;
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


    #region SAVING METHODS

    public void SaveSelectedCar(int currentlySelectedCar)
    {
        selectedCarIndex = currentlySelectedCar;
        SaveGame.Save<int>(SELECTEDCAR, selectedCarIndex);
    }

    #endregion



}
