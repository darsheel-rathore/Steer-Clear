using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallManager : MonoBehaviour
{
    public delegate void OnPlayerFellDownFromTrack();
    public static OnPlayerFellDownFromTrack playerFellFromTrack;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerFellFromTrack != null)
                playerFellFromTrack();

            HandlePlayerFall(other);
        }
    }


    private void HandlePlayerFall(Collider value)
    {
        Destroy(value.GetComponentInParent<TCCAPlayer>().gameObject);
    }

}
