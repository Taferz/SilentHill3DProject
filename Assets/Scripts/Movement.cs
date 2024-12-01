using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Speed of forward and backward movement
    public float runSpeed = 10.0f; // Speed of running
    public float turnSpeed = 100.0f; // Speed of turning
    private Animator animator; // Reference to the Animator component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = false;
        bool isRunning = false;
        bool isWalkingBackward = false;
        bool isTurningLeft = false;
        bool isTurningRight = false;

        // Move forward
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
                isRunning = true;
            }
            else
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                isWalking = true;
            }
        }

        // Move backward
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
            isWalkingBackward = true;
        }

        // Turn left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
            isTurningLeft = true;
        }

        // Turn right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
            isTurningRight = true;
        }

        // Update the animator with the walking, running, walking backward, and turning states
        if (animator != null)
        {
            animator.SetBool("isWalking", isWalking);
            animator.SetBool("isRunning", isRunning);
            animator.SetBool("isWalkingBackward", isWalkingBackward);
            animator.SetBool("isTurningLeft", isTurningLeft);
            animator.SetBool("isTurningRight", isTurningRight);
        }
    }
}