using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningStage : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> winningParticles;

    public delegate void RaceFinished();
    public static RaceFinished raceFinished;

    private void OnTriggerEnter(Collider other)
    {
        // when player entered the winning stage
        if (other.CompareTag("Player"))
        {
            // play all particles
            foreach (ParticleSystem p in winningParticles)
                p.Play();

            if (raceFinished != null)
                raceFinished();

            // caching player and controller script
            TCCAPlayer player = other.GetComponentInParent<TCCAPlayer>();
            ControllerSelf controller = player.GetComponentInChildren<ControllerSelf>();
            
            // stopping and turning the player
            controller.SetInputHorizontal(1);
            controller.SetInputVertical(0);
            controller.SethandBrakeValue(true);
            player.GetComponentInChildren<InputListener>().enabled = false;

        }
    }
}
