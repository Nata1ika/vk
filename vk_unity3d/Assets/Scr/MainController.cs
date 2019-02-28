using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainController : MonoBehaviour
{
    public static System.Action<Target> ChangeTargetEvent;

    [SerializeField] Camera _camera;
    [SerializeField] CinemachineCameraController _cinemachine;

    private bool _activeTarget = false;
    private Target _target;
    private float _timeTarget = 0;
    private int _countClickLast = 0;

    private const float LONG_CLICK = 1f;

    private void Update()
    {
        if (_activeTarget)
        {
            return;
        }

        //начало клика
        if (!IsPointerOverUIObject() && _target == null &&
            (Input.GetMouseButtonDown(0) || _countClickLast == 0 && Input.touchCount == 1))
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, LayerMask.GetMask("target"));
            Target variant = GetTarget(hits);

            if (variant != null)
            {
                _target = variant;
                _timeTarget = 0;
            }
            else
            {
                ActivateCamera(true);
            }
        }
        //продолжение клика
        else if (Input.GetMouseButton(0) || Input.touchCount >= 1)
        {
            if (_target != null)
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, LayerMask.GetMask("target"));
                Target variant = GetTarget(hits);
                _timeTarget += Time.deltaTime;
                if (variant != _target || _timeTarget > LONG_CLICK)
                {
                    ActivateCamera(true);
                    _target = null;
                }
            }
        }
        else
        {
            if (_target != null && _timeTarget < LONG_CLICK)
            {
                ChangeTargetEvent?.Invoke(_target);
                _activeTarget = true;
                _target = null;
            }

            ActivateCamera(false);
        }

        _countClickLast = Input.touchCount;
    }

    public void DeactivateTarget()
    {
        _activeTarget = false;
        ChangeTargetEvent?.Invoke(null);
    }

    private void ActivateCamera(bool active)
    {
        //var system = SystemInfo.operatingSystem.ToLower();
        //if ((system.Contains("ios") || system.Contains("android"))

        _cinemachine.Active = active;
    }

    private Target GetTarget(RaycastHit[] hits)
    {
        Target result = null;
        float _targetDistance = 0;
        float sphereDistance = -1;
        for (int i = 0; i < hits.Length; i++)
        {
            Target variant = hits[i].collider.gameObject.GetComponent<Target>();
            if (variant == null)
            {
                sphereDistance = hits[i].distance;
            }
            else if (result == null || _targetDistance < hits[i].distance)
            {
                result = variant;
            }
        }
        /*
        if (sphereDistance > 0 && _targetDistance < sphereDistance + 1)
        {
            return null;
        }
        */

        return result;
    }

    public static bool IsPointerOverUIObject()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.touches[i].fingerId))
            {
                return true;
            }
        }
        return EventSystem.current.IsPointerOverGameObject();
    }
}
