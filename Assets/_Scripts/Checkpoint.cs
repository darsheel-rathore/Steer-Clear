using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject displayParticle;
    [SerializeField] private GameObject pickedupPartice;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // vfx handle
            displayParticle.SetActive(false);
            pickedupPartice.GetComponent<ParticleSystem>().Play();
            Invoke("ParticleDisableInvoke", 1f);

            // set last position and rotation
            SetLastCheckpointAndRotation();
        }
    }

    private void ParticleDisableInvoke()
    {
        pickedupPartice.SetActive(false);
    }

    private void SetLastCheckpointAndRotation()
    {
        GetComponentInParent<CheckpointManager>().SetLastCheckPointPosition(transform.position);
        GetComponentInParent<CheckpointManager>().SetLastCheckPointRotation(transform.rotation);
    }

}
