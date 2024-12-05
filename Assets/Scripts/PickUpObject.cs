using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public GameObject myHands; // Reference to your hands/the position where you want your object to go
    public GameObject handPosition; // Reference to the hidden object in the hand
    private bool canPickup; // A bool to see if you can or can't pick up the item
    public GameObject objectIWantToPickUp; // The GameObject on which you collided with
    private bool hasItem; // A bool to see if you have an item in your hand

    void Start()
    {
        canPickup = false; // Setting both to false
        hasItem = false;
    }

    void Update()
    {
        if (canPickup && !hasItem) // If you enter the collider of the object and don't have an item
        {
            if (Input.GetKeyDown(KeyCode.E)) // Can be 'E' or any key
            {
                PickUp();
            }
        }
    }

    private void PickUp()
    {
        objectIWantToPickUp.transform.position = handPosition.transform.position; // Sets the position of the object to the hand position
        objectIWantToPickUp.transform.rotation = handPosition.transform.rotation; // Sets the rotation of the object to the hand rotation
        objectIWantToPickUp.transform.parent = myHands.transform; // Makes the object become a child of the parent so that it moves with the hands
        hasItem = true; // Update the hasItem flag
        Debug.Log("Picked up the object: " + objectIWantToPickUp.name);
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
            objectIWantToPickUp = null; // Clear the reference to the object
            Debug.Log("Exited trigger with object");
        }
    }
}