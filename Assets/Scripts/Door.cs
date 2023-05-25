using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string color;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Get the PlayerController component from the collided player object
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            // Check if the player has the corresponding key to open the door
            if (player.HasKey(color))
            {
                OpenDoor();
                // Remove the key from the player's inventory
                player.RemoveKey(color);
            }
        }
    }

    private void OpenDoor()
    {
        // Get all the colliders attached to the door
        Collider[] doorCollider = GetComponents<Collider>();

        // Destroy each collider to open the door
        for (int i = 0; i < doorCollider.Length; i++)
        {
            Destroy(doorCollider[i]);
        }
    }
}
