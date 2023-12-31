﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // singleton instance
    public static SceneManagement instance;

    // vars
    [SerializeField] private int mainMenuSceneIndex;
    [SerializeField] private int lastSceneIndex;

    private void Awake()
    {
        // singleton setup
        if (SceneManagement.instance == null)
        {
            SceneManagement.instance = this;
        }
        else
        {
            if (SceneManagement.instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    #region Public methods

    public void ReloadCurrentScene()
    {
        int currentSceneIndex = GetCurrentSceneIndex();
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = GetCurrentSceneIndex();
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void MainMenuPlayButton()
    {
        int currentlyActiveGameScene = 1;
        SceneManager.LoadScene(currentlyActiveGameScene);
    }

    public void HomeButton()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadCustomLevel(int levelIndex)
    {
        // load level according to the button
        SceneManager.LoadScene(levelIndex);
    }

    #endregion


    #region Private Methods

    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    #endregion
}
