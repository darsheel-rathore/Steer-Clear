using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roadsideblocks : MonoBehaviour
{
    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.Sleep();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // wake rb on collision with player
        if (collision.gameObject.CompareTag("Player"))
        {
            if (rigidBody.IsSleeping())
                rigidBody.WakeUp();
        }
    }
}
