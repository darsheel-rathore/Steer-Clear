using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonIdCarrier : MonoBehaviour
{

    private int levelId;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => LoadDesiredLevel());
    }

    public void SetLevelID(int id)
    {
        levelId = id;
    }

    public void LoadDesiredLevel()
    {
        SceneManagement.instance.LoadCustomLevel(levelId);
    }
}
