using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Speed of forward and backward movement
    public float turnSpeed = 100.0f; // Speed of turning

    // Update is called once per frame
    void Update()
    {
        // Move forward
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        // Move backward
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        // Turn left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }

        // Turn right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }
    }
}