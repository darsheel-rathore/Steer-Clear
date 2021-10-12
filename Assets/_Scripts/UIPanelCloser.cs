using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelCloser : MonoBehaviour
{
    [SerializeField] private GameObject panelToClose;

    public void _CloseButtonClicked()
    {
        panelToClose.SetActive(false);

        // normalizing time if game is paused
        Time.timeScale = Time.timeScale != 1f ? 1f : Time.timeScale;
    }
}
