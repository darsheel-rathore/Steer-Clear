using BayatGames.SaveGameFree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStampTest : MonoBehaviour
{
    private float timeStamp;

    //private void Awake()
    //{
    //    Debug.Log(DateTime.Now);
    //    timeStamp = Environment.TickCount;
    //    Debug.Log("Awake Time Stamp: " + Environment.TickCount);
    //}

    //// Start is called before the first frame update
    //void Start()
    //{
    //    Debug.Log("Start time stamp = " + Environment.TickCount);
    //    Debug.Log("Time Taken between awake and start: " + (Environment.TickCount - timeStamp));
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    private void Start()
    {
        SaveGame.Clear();
    }

}
