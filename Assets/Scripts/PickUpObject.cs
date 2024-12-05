using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public GameObject myHands; // Reference to your hands/the position where you want your object to go
    public GameObject handPosition; // Reference to the hidden object in the hand
    public GameObject objectIWantToPickUp; // The GameObject you want to pick up, referenced in the Inspector
    public Material highlightMaterial1; // Reference to the first highlight material
    public Material highlightMaterial2; // Reference to the second highlight material
    private Material originalMaterial1; // Reference to the first original material of the object
    private Material originalMaterial2; // Reference to the second original material of the child object
    private Renderer parentRenderer; // Reference to the Renderer component of the parent object
    private Renderer childRenderer; // Reference to the Renderer component of the child object
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

        if (objectIWantToPickUp != null)
        {
            parentRenderer = objectIWantToPickUp.GetComponent<Renderer>();
            if (parentRenderer != null)
            {
                originalMaterial1 = parentRenderer.material; // Get the first original material of the parent object
            }

            if (objectIWantToPickUp.transform.childCount > 0)
            {
                childRenderer = objectIWantToPickUp.transform.GetChild(0).GetComponent<Renderer>();
                if (childRenderer != null)
                {
                    originalMaterial2 = childRenderer.material; // Get the first original material of the child object
                }
            }
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
        HighlightObject(false); // Ensure the object uses the original materials after being picked up
    }

    private void OnTriggerEnter(Collider other) // To see when the player enters the collider
    {
        if (other.gameObject.CompareTag("object")) // On the object you want to pick up, set the tag to be anything, in this case "object"
        {
            canPickup = true; // Set the pickup bool to true
            Debug.Log("Entered trigger with object: " + objectIWantToPickUp.name);
            HighlightObject(true); // Highlight the object
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("object"))
        {
            canPickup = false; // When you leave the collider, set the canPickup bool to false
            Debug.Log("Exited trigger with object");
            HighlightObject(false); // Remove the highlight from the object
        }
    }

    private void HighlightObject(bool highlight)
    {
        if (objectIWantToPickUp != null)
        {
            if (parentRenderer != null)
            {
                parentRenderer.material = (highlight && !hasItem) ? highlightMaterial1 : originalMaterial1; // Change the material of the parent object based on the highlight flag
            }

            if (childRenderer != null)
            {
                childRenderer.material = (highlight && !hasItem) ? highlightMaterial2 : originalMaterial2; // Change the material of the child object based on the highlight flag
            }
        }
    }

    public bool HasItem()
    {
        return hasItem;
    }
}