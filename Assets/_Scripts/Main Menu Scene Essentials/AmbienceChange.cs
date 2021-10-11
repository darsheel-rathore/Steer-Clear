using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceChange : MonoBehaviour
{

    [SerializeField] private List<Material> ambienceMaterialList;

    // to hold the current ambient index
    private int currnetAmbientIndex;

    // Start is called before the first frame update
    void Start()
    {
        // get the current ambient index of the scene
        for (int i = 0; i < ambienceMaterialList.Count; i++)
        {
            if (ambienceMaterialList[i] == RenderSettings.skybox)
            {
                currnetAmbientIndex = i;
                break;
            }
        }
    }


    public void _ChangeSkyboxMaterial()
    {
        RenderSettings.skybox = ambienceMaterialList[GetRandomInteger()];
    }

    private int GetRandomInteger()
    {
        int randomNumber = Random.Range(0, ambienceMaterialList.Count);
        return randomNumber == currnetAmbientIndex ? GetRandomInteger() : randomNumber;
    }
}
