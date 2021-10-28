using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnAudioSource : MonoBehaviour
{

    public static BtnAudioSource instance;

    private void Awake()
    {
        if (BtnAudioSource.instance == null)
            BtnAudioSource.instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
