using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VCHandler : MonoBehaviour
{
    public static VCHandler instace;

    CinemachineVirtualCamera vc;

    private void Awake()
    {
        if (VCHandler.instace == null)
            VCHandler.instace = this;

        vc = GetComponent<CinemachineVirtualCamera>();
    }


    public void SetVCFollowTarget(Transform targetTransform)
    {
        vc.Follow = targetTransform;
    }
}
