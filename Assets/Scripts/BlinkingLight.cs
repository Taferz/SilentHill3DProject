using UnityEngine;
using System.Collections;

public class BlinkingLight : MonoBehaviour
{
    public Light lightObject; // Reference to the light object
    public float minInterval = 0.1f; // Minimum time interval in seconds
    public float maxInterval = 1.0f; // Maximum time interval in seconds

    private void Start()
    {
        if (lightObject == null)
        {
            Debug.LogError("Light object is not assigned.");
            return;
        }

        // Start the coroutine to toggle the light
        StartCoroutine(FlickerLight());
    }

    private IEnumerator FlickerLight()
    {
        while (true)
        {
            // Wait for a random interval between minInterval and maxInterval
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            // Randomly turn the light on or off
            lightObject.enabled = !lightObject.enabled;
        }
    }
}