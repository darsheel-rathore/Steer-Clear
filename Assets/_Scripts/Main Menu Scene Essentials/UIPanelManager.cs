using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelManager : MonoBehaviour
{
    // panel collection
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject levelSelectionPanel;
    [SerializeField] private GameObject adRewardPanel;
    [SerializeField] private GameObject gameQuitPanel;


    // public UI callback methods
    public void _OnSettingPanelButtonClicked()
    {
        settingsPanel.SetActive(true);
    }

    public void _OnLevelSelectionPanelClicked()
    {
        levelSelectionPanel.SetActive(true);
    }
   
    public void _OnAdRewardButtonClicked()
    {
        adRewardPanel.SetActive(true);
    }

    public void _OnGameQuitButtonClicked()
    {
        gameQuitPanel.SetActive(true);
    }

    public void _OnExitButtonCancelClicked()
    {
        gameQuitPanel.SetActive(false);
    }



}
