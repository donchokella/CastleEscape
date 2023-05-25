using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string color;    // Color of the key

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the colliding object is the player
        {
            PlayerController player = other.GetComponent<PlayerController>();   // Get the PlayerController component from the player

            if (player != null)
            {
                player.AddKey(color);   // Add the key to the player's inventory
            }

            Destroy(gameObject);    // Destroy the key game object
        }
    }
}
