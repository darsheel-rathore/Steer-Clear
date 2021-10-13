using BayatGames.SaveGameFree;
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

    private Dictionary<int, string> levelTiming;
    private Dictionary<int, float> totalLevelTimings;

    //[Header("Method 2")]
    //[SerializeField] private GameObject lockedLevelPrefab;
    //[SerializeField] private GameObject unlockedLevelPrefab;

    //private List<GameObject> lockedLevelInstantiatedList;
    //private List<GameObject> unlockedLevelInstantiatedList;

    private void Start()
    {
        // initializing dictionaries
        levelTiming = new Dictionary<int, string>();
        totalLevelTimings = new Dictionary<int, float>();

        levelTiming = SaveGame.Load<Dictionary<int, string>>("LevelTimings");
        totalLevelTimings = SaveGame.Load<Dictionary<int, float>>("LevelTotalTimings");

        // initializing prefab holder list
        InstantiatePrefabs();

        
    }

    private void InstantiatePrefabs()
    {
        instantiatedLevelPrefabs = new List<GameObject>();

        for (int i = 0; i < countOfLevelsToBeGenerated; i++)
        {
            // instantiate and add to the list
            instantiatedLevelPrefabs.Add(Instantiate(prefabToBeInstantiate, instantiationArea) as GameObject);
            // attach audio source and level id to the buttons
            instantiatedLevelPrefabs[i].GetComponent<Assets.Plugins.ButtonSoundsEditor.ButtonClickSound>().AudioSource = buttonAudioSource;
            instantiatedLevelPrefabs[i].GetComponent<LevelButtonIdCarrier>().SetLevelID(i+1);


            // incase the instantiated level is unlocked
            if (i <= (unlockedLevels - 1))
            {
                // grabbing text components and assigning respective values
                TextMeshProUGUI[] texts = new TextMeshProUGUI[2];
                texts = instantiatedLevelPrefabs[i].GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = "Lv. " + (i + 1);
                texts[1].text = levelTiming[i];

                // grabbing all the image components of the prefab and disabling the last one 
                // as it is the one with the locked icon on it
                int componentCountWithImage = 0;

                // counting the total number of image components as children
                foreach (Transform g in instantiatedLevelPrefabs[i].GetComponentsInChildren<Transform>())
                {
                    if (g.GetComponent<Image>() != null)
                        componentCountWithImage += 1;
                }

                // create an image array and assign the childs with image components
                Image[] img = new Image[componentCountWithImage];
                img = instantiatedLevelPrefabs[i].GetComponentsInChildren<Image>();

                // cycle through all the image components and enable the last one
                for (int x = 0; x < img.Length; x++)
                {
                    if (x == (img.Length - 1))
                        img[x].enabled = false;
                }
            }
            // incase where the instantiated level is locked
            else
            {
                // array for storing locked childs
                GameObject[] prefabChilds = new GameObject[instantiatedLevelPrefabs[i].GetComponentsInChildren<Transform>().Length];

                // instantiate and assign the array childs
                for (int y = 0; y < prefabChilds.Length; y++)
                {
                    prefabChilds[y] = instantiatedLevelPrefabs[i].GetComponentsInChildren<Transform>()[y].gameObject;
                }

                // cycle through all the locked array childs for first disable all the image
                // then enable the last and first one
                for (int x = 0; x < prefabChilds.Length; x++)
                {
                    // for disabling all the image components but the first
                    prefabChilds[x].SetActive(false);
                    if (x == 0)
                    {
                        prefabChilds[x].SetActive(true);
                        prefabChilds[x].GetComponent<Image>().enabled = false;
                    }
                    // enable the last one
                    else if (x == (prefabChilds.Length - 1))
                    {
                        prefabChilds[x].SetActive(true);
                        prefabChilds[x].GetComponent<Image>().enabled = true;
                    }
                }
            }
        }
    }




    // =================================================

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
