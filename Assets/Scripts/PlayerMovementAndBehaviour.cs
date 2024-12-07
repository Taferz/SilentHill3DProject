using UnityEngine;

public class PlayerMovementAndBehaviour : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Speed of forward and backward movement
    public float runSpeed = 10.0f; // Speed of running
    public float turnSpeed = 100.0f; // Speed of turning
    public int weaponDamage = 50; // Damage dealt by the weapon
    private Animator animator; // Reference to the Animator component
    private PickUpObject pickUpObject; // Reference to the PickUpObject component
    private ZombieDeath zombieDeath; // Reference to the HealthSystem component
    private bool hasPickedUpItem = false; // Track if the item has been picked up

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component is not assigned.");
        }

        pickUpObject = GetComponent<PickUpObject>();
        if (pickUpObject == null)
        {
            Debug.LogError("PickUpObject component is not assigned.");
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

        // Check if the item has been picked up
        if (pickUpObject != null && pickUpObject.HasItem())
        {
            hasPickedUpItem = true;
        }

        // Shoot
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
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

    void Shoot()
    {
        if (hasPickedUpItem)
        {
            // Trigger the shoot animation
            if (animator != null)
            {
                animator.SetTrigger("Shoot");
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                ZombieDeath targetHealth = hit.transform.GetComponent<ZombieDeath>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(weaponDamage); // Apply damage
                }
            }
        }
    }
}