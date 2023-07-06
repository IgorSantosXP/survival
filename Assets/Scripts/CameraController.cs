using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 300f;
    [SerializeField] private float verticalSpeed = 2f;
    [SerializeField] private float zoomSpeed = 15f;
    [SerializeField] private float minZoom = 20f;
    [SerializeField] private float maxZoom = 60f;

    public bool isCameraMoving = false;

    private Cinemachine.CinemachineFreeLook freeLookCamera;

    private void Start()
    {
        freeLookCamera = GetComponent<Cinemachine.CinemachineFreeLook>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            freeLookCamera.m_XAxis.m_MaxSpeed = horizontalSpeed;
            freeLookCamera.m_YAxis.m_MaxSpeed = verticalSpeed;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            isCameraMoving = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            freeLookCamera.m_XAxis.m_MaxSpeed = 0f;
            freeLookCamera.m_YAxis.m_MaxSpeed = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            isCameraMoving = false;
        }

        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        float newZoom = freeLookCamera.m_Lens.FieldOfView - zoomInput * zoomSpeed;
        newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);
        freeLookCamera.m_Lens.FieldOfView = newZoom;
    }
}
