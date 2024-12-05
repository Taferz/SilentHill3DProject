using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchBehaviour : MonoBehaviour
{
    public GameObject LockOnObject;
    public Camera[] cameras;
    public int currentCameraIndex = 0;
    public Vector3 lockOnOffset; // Offset for the camera lock-on

    public MainThirdPersonCamera thirdPersonCamera; // Reference to the third-person camera script

    void Start()
    {
        SetActiveCamera(currentCameraIndex);
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
            bool isActive = (i == index);
            cameras[i].enabled = isActive;
            AudioListener audioListener = cameras[i].GetComponent<AudioListener>();
            if (audioListener != null)
            {
                audioListener.enabled = isActive;
            }
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
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].enabled = false;
            AudioListener audioListener = cameras[i].GetComponent<AudioListener>();
            if (audioListener != null)
            {
                audioListener.enabled = false;
            }
        }

        if (thirdPersonCamera != null)
        {
            thirdPersonCamera.enabled = true;
            Camera mainCamera = thirdPersonCamera.GetComponent<Camera>();
            if (mainCamera != null)
            {
                mainCamera.enabled = true;
                AudioListener mainAudioListener = mainCamera.GetComponent<AudioListener>();
                if (mainAudioListener != null)
                {
                    mainAudioListener.enabled = true;
                }
            }
        }
    }
}