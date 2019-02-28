using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCamera : MonoBehaviour
{
    [SerializeField] Cinemachine.CinemachineVirtualCamera _targetCamera;

    private void Start()
    {
        MainController.ChangeTargetEvent += ChangeTarget;
    }

    private void OnDestroy()
    {
        MainController.ChangeTargetEvent -= ChangeTarget;
    }

    private void ChangeTarget(Target obj)
    {
        if (obj == null)
        {
            _targetCamera.Priority = 5;
            return;
        }

        _targetCamera.Follow = obj.gameObject.transform;
        _targetCamera.LookAt = obj.gameObject.transform;
        _targetCamera.m_Priority = 20;
        _targetCamera.Priority = 20;
    }
}
