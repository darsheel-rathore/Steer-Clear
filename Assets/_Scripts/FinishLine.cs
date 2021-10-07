using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public delegate void RaceFinished();
    public static RaceFinished raceFinished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (raceFinished != null)
                raceFinished();
        }
    }
}
