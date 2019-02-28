using Cinemachine;
using System.Collections;
using UnityEngine;

public class CinemachineCameraController : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook _cinemachine;
    [SerializeField] SimpleFollowRecenter _recenter;

    private bool _isActive = true;
    public bool Active
    {
        get
        {
            return _isActive;
        }
        set
        {
            _isActive = value;
            _cinemachine.m_XAxis.m_InputAxisName = _isActive ? "Mouse X" : "";
            _cinemachine.m_YAxis.m_InputAxisName = _isActive ? "Mouse Y" : "";

            _cinemachine.m_XAxis.m_InputAxisValue = 0;
            _cinemachine.m_YAxis.m_InputAxisValue = 0;
        }
    }

    public void SetDefault()
    {
        _recenter.recenter = true;
        _cinemachine.m_YAxis.Value = 0.5f;
    }
}
