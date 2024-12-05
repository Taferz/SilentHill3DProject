using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public GameObject myHands; // Reference to your hands/the position where you want your object to go
    public GameObject handPosition; // Reference to the hidden object in the hand
    public GameObject objectIWantToPickUp; // The GameObject you want to pick up, referenced in the Inspector
    private bool canPickup; // A bool to see if you can or can't pick up the item
    private bool hasItem; // A bool to see if you have an item in your hand
    private Animator animator; // Reference to the Animator component

    void Start()
    {
        canPickup = false; // Setting both to false
        hasItem = false;
        animator = GetComponent<Animator>(); // Get the Animator component
        if (animator == null)
        {
            Debug.LogError("Animator component is not assigned.");
        }
    }

    void Update()
    {
        if (canPickup && !hasItem) // If you enter the collider of the object and don't have an item
        {
            if (Input.GetKeyDown(KeyCode.E)) // Can be 'E' or any key
            {
                Debug.Log("E key pressed");
                PickUp();
            }
        }
    }

    private void PickUp()
    {
        Debug.Log("PickUp method called");
        animator.SetBool("isPickingUp", true); // Set the pickup boolean to true
        StartCoroutine(PickUpCoroutine());
    }

    private IEnumerator PickUpCoroutine()
    {
        Debug.Log("PickUpCoroutine started");
        yield return new WaitForSeconds(0.5f); // Wait for the animation to play (adjust the duration as needed)
        objectIWantToPickUp.transform.position = handPosition.transform.position; // Sets the position of the object to the hand position
        objectIWantToPickUp.transform.rotation = handPosition.transform.rotation; // Sets the rotation of the object to the hand rotation
        objectIWantToPickUp.transform.parent = myHands.transform; // Makes the object become a child of the parent so that it moves with the hands
        hasItem = true; // Update the hasItem flag
        animator.SetBool("isPickingUp", false); // Set the pickup boolean to false
        Debug.Log("Picked up the object: " + objectIWantToPickUp.name);
        Debug.Log("Object position: " + objectIWantToPickUp.transform.position);
        Debug.Log("Object parent: " + objectIWantToPickUp.transform.parent.name);
    }

    private void OnTriggerEnter(Collider other) // To see when the player enters the collider
    {
        if (other.gameObject.CompareTag("object")) // On the object you want to pick up, set the tag to be anything, in this case "object"
        {
            canPickup = true; // Set the pickup bool to true
            Debug.Log("Entered trigger with object: " + objectIWantToPickUp.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("object"))
        {
            canPickup = false; // When you leave the collider, set the canPickup bool to false
            Debug.Log("Exited trigger with object");
        }
    }
}