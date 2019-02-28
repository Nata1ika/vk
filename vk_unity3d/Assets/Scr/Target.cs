using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] Material _active;
    [SerializeField] Material _usual;

    private MeshRenderer _renderer;
    private bool _isActive = false;

    private void Start()
    {
        _renderer = gameObject.GetComponent<MeshRenderer>();
        MainController.ChangeTargetEvent += ChangeTarget;
    }

    private void OnDestroy()
    {
        MainController.ChangeTargetEvent -= ChangeTarget;
    }

    private void ChangeTarget(Target obj)
    {
        if (obj == this)
        {
            _renderer.material = _active;
            _isActive = true;
        }
        else if (_isActive)
        {
            _renderer.material = _usual;
            _isActive = false;
        }
    }
}
