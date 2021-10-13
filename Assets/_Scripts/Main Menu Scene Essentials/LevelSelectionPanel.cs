using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionPanel : MonoBehaviour
{
    [Header("Instantiation Objects and Transforms")]
    [SerializeField] private Transform instantiationArea;
    [SerializeField] private GameObject prefabToBeInstantiate;

    [Header("Level Details")]
    [SerializeField] private int countOfLevelsToBeGenerated;
    [SerializeField] private int unlockedLevels;

    [Header("Audio Source For Button")]
    [SerializeField] private AudioSource buttonAudioSource;

    private List<GameObject> instantiatedLevelPrefabs;

    //[Header("Method 2")]
    //[SerializeField] private GameObject lockedLevelPrefab;
    //[SerializeField] private GameObject unlockedLevelPrefab;

    //private List<GameObject> lockedLevelInstantiatedList;
    //private List<GameObject> unlockedLevelInstantiatedList;

    private void Start()
    {
        // initializing prefab holder list
        InstantiatePrefabs();
    }

    private void InstantiatePrefabs()
    {
        instantiatedLevelPrefabs = new List<GameObject>();

        for (int i = 0; i < countOfLevelsToBeGenerated; i++)
        {
            instantiatedLevelPrefabs.Add(Instantiate(prefabToBeInstantiate, instantiationArea) as GameObject);
            instantiatedLevelPrefabs[i].GetComponent<Assets.Plugins.ButtonSoundsEditor.ButtonClickSound>().AudioSource = buttonAudioSource;
            instantiatedLevelPrefabs[i].GetComponent<LevelButtonIdCarrier>().SetLevelID(i+1);


            // incase the instantiated level is unlocked
            if (i <= (unlockedLevels - 1))
            {
                // grabbing text components and assigning respective values
                TextMeshProUGUI[] texts = new TextMeshProUGUI[2];
                texts = instantiatedLevelPrefabs[i].GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = "Lv. " + (i + 1);
                texts[1].text = "0:00";

                // grabbing all the image components of the prefab and disabling the last one 
                // as it is the one with the locked icon on it
                int componentCountWithImage = 0;

                foreach (Transform g in instantiatedLevelPrefabs[i].GetComponentsInChildren<Transform>())
                {
                    if (g.GetComponent<Image>() != null)
                        componentCountWithImage += 1;
                }

                Image[] img = new Image[componentCountWithImage];
                img = instantiatedLevelPrefabs[i].GetComponentsInChildren<Image>();

                for (int x = 0; x < img.Length; x++)
                {
                    if (x == (img.Length - 1))
                        img[x].enabled = false;
                }
            }
            // incase where the instantiated level is locked
            else
            {
                GameObject[] prefabChilds = new GameObject[instantiatedLevelPrefabs[i].GetComponentsInChildren<Transform>().Length];

                for (int y = 0; y < prefabChilds.Length; y++)
                {
                    prefabChilds[y] = instantiatedLevelPrefabs[i].GetComponentsInChildren<Transform>()[y].gameObject;
                }

                for (int x = 0; x < prefabChilds.Length; x++)
                {
                    // for disabling all the gameobjects
                    prefabChilds[x].SetActive(false);
                    if (x == 0)
                    {
                        prefabChilds[x].SetActive(true);
                        prefabChilds[x].GetComponent<Image>().enabled = false;
                    }
                    else if (x == (prefabChilds.Length - 1))
                    {
                        prefabChilds[x].SetActive(true);
                        prefabChilds[x].GetComponent<Image>().enabled = true;
                    }
                }
            }
        }
    }




    #region Unused Code
    //private void InstantiatePrefabAnotherMethod()
    //{
    //    // initializing both the lists to hold prefabs
    //    lockedLevelInstantiatedList = new List<GameObject>();
    //    unlockedLevelInstantiatedList = new List<GameObject>();

    //    for (int i = 0; i < countOfLevelsToBeGenerated; i++)
    //    {
    //        if (i <= unlockedLevels)
    //        {
    //            unlockedLevelInstantiatedList.Add(Instantiate(unlockedLevelPrefab, instantiationArea) as GameObject);

    //            // grab all the text components
    //            TextMeshProUGUI[] texts = unlockedLevelInstantiatedList[i].GetComponentsInChildren<TextMeshProUGUI>();

    //            texts[0].text = "Lv. " + (i + 1);
    //            texts[1].text = "0:00";
    //        }
    //        else
    //        {
    //            lockedLevelInstantiatedList.Add(Instantiate(lockedLevelPrefab, instantiationArea) as GameObject);
    //        }
    //    }
    //}
    #endregion
}
