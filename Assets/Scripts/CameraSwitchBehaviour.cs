using UnityEngine;

public class CameraSwitchBehaviour : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    private Camera currentCamera; // Reference to the current active camera
    private Camera previousCamera; // Reference to the previous camera

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Ensure all cameras are turned off except the main camera
        Camera[] allCameras = Camera.allCameras;
        foreach (Camera cam in allCameras)
        {
            if (cam != mainCamera)
            {
                cam.gameObject.SetActive(false);
            }
        }

        // Set the main camera as the current camera
        currentCamera = mainCamera;
        currentCamera.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Optional: Add any additional logic if needed
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider has a camera attached
        Camera colliderCamera = other.GetComponentInChildren<Camera>();
        if (colliderCamera != null)
        {
            // Switch to the collider's camera
            previousCamera = currentCamera;
            currentCamera.gameObject.SetActive(false);
            currentCamera = colliderCamera;
            currentCamera.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the collider has a camera attached
        Camera colliderCamera = other.GetComponentInChildren<Camera>();
        if (colliderCamera != null && currentCamera == colliderCamera)
        {
            // Switch back to the previous camera
            currentCamera.gameObject.SetActive(false);
            currentCamera = previousCamera;
            currentCamera.gameObject.SetActive(true);
        }
    }
}