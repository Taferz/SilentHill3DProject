using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public CameraSwitchBehaviour cameraManager;
    [SerializeField] private Camera nextCamera;
    private Camera previousCamera;
    private int state = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (state == 0)
            {
                previousCamera = cameraManager.cameras[cameraManager.currentCameraIndex];
                cameraManager.SetActiveCamera(cameraManager.GetCameraIndex(nextCamera));
                Debug.Log("Switched to next camera");
                state = 1;
            }
            else
            {
                cameraManager.SetActiveCamera(cameraManager.GetCameraIndex(previousCamera));
                Debug.Log("Switched to previous camera");
                state = 0;
            }
        }
    }
}