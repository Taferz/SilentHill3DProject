using UnityEngine;

public class MainThirdPersonCamera : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float distance = 5.0f; // Distance from the player
    public float height = 2.0f; // Height above the player
    public float smoothSpeed = 0.125f; // Smoothing speed for camera movement
    public LayerMask collisionMask; // Layer mask for collision detection
    public float cameraRadius = 0.5f; // Radius for the sphere cast
    public float lookAtHeightOffset = 1.0f; // Height offset for the LookAt target

    private Vector3 offset;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player transform is not assigned.");
            return;
        }

        offset = new Vector3(0, height, -distance);
    }

    private void LateUpdate()
    {
        if (player == null) return;

        Vector3 desiredPosition = player.position + player.TransformDirection(offset);
        Vector3 direction = desiredPosition - player.position;

        // Check for collisions using SphereCast
        if (Physics.SphereCast(player.position, cameraRadius, direction, out RaycastHit hit, direction.magnitude, collisionMask))
        {
            desiredPosition = player.position + direction.normalized * (hit.distance - cameraRadius);
        }

        // Smoothly move the camera to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Always look at the center of the player with a height offset
        Vector3 lookAtPosition = player.position + Vector3.up * lookAtHeightOffset;
        transform.LookAt(lookAtPosition);
    }
}