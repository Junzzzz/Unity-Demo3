using System;
using Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLookCamera;

    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float zoomAcceleration = 2f;
    [SerializeField] private float zoomInnerRange = 3f;
    [SerializeField] private float zoomOuterRange = 15f;
    [SerializeField] private float zoomYAxis = 0f;

    private float _currentMiddleRigRadius;
    private float _newMiddleRigRadius;

    private void Start()
    {
        if (freeLookCamera == null)
        {
            freeLookCamera = GetComponent<CinemachineFreeLook>();
        }

        _currentMiddleRigRadius = freeLookCamera.m_Orbits[1].m_Radius;
    }

    private void FixedUpdate()
    {
        zoomYAxis = Input.GetAxis("Mouse ScrollWheel");
        AdjustCameraZoomIndex(zoomYAxis);
        UpdateZoomLevel();
    }

    private void UpdateZoomLevel()
    {
        if (Math.Abs(_currentMiddleRigRadius - _newMiddleRigRadius) < 0.01f)
        {
            return;
        }

        _currentMiddleRigRadius =
            Mathf.Lerp(_currentMiddleRigRadius, _newMiddleRigRadius, zoomAcceleration * Time.deltaTime);
        _currentMiddleRigRadius = Mathf.Clamp(_currentMiddleRigRadius, zoomInnerRange, zoomOuterRange);

        freeLookCamera.m_Orbits[1].m_Radius = _currentMiddleRigRadius;
        freeLookCamera.m_Orbits[0].m_Height = _currentMiddleRigRadius;
        freeLookCamera.m_Orbits[2].m_Height = -_currentMiddleRigRadius;
    }

    private void AdjustCameraZoomIndex(float zoomYAxis)
    {
        switch (zoomYAxis)
        {
            case 0:
                return;
            case < 0:
                _newMiddleRigRadius = _currentMiddleRigRadius + zoomSpeed;
                break;
            case > 0:
                _newMiddleRigRadius = _currentMiddleRigRadius - zoomSpeed;
                break;
        }
    }
}