using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public CameraSwitchBehaviour cameraManager;
    [SerializeField] private Camera nextCamera;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraManager.SetActiveCamera(cameraManager.GetCameraIndex(nextCamera));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraManager.DisableAllButMain();
        }
    }
}