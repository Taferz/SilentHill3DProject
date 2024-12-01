using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchBehaviour : MonoBehaviour
{
    public GameObject LockOnObject;
    public Camera[] cameras;
    public int currentCameraIndex = 0;
    public Vector3 lockOnOffset; // Offset for the camera lock-on

    private MainThirdPersonCamera thirdPersonCamera;

    void Start()
    {
        SetActiveCamera(currentCameraIndex);
        thirdPersonCamera = gameObject.AddComponent<MainThirdPersonCamera>();
    }

    void Update()
    {
        if (LockOnObject != null)
        {
            LockCameraOnPlayer();
        }
    }

    public void LockCameraOnPlayer()
    {
        Vector3 lookAtPosition = LockOnObject.transform.position + lockOnOffset;
        cameras[currentCameraIndex].transform.LookAt(lookAtPosition);
    }

    public void SetActiveCamera(int index)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].enabled = (i == index);
        }
        currentCameraIndex = index;
    }

    public int GetCameraIndex(Camera cam)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == cam)
            {
                return i;
            }
        }
        return -1;
    }

    public void DisableAllButMain()
    {
        if (Camera.main != null)
        {
            Camera mainCamera = Camera.main;
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].enabled = false;
            }
            mainCamera.enabled = true;
        }
    }
}