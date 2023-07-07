using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrader : MonoBehaviour
{
    [SerializeField] private int upgradePower;    // Power of the upgrade
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))     // Check if the colliding object is the player
        {
            PlayerController playerController = other.GetComponent<PlayerController>();     // Get the PlayerController component from the player

            if (playerController != null)
            {
                playerController.IncreasePlayerLevel(upgradePower); // Increase the player's level with the specified upgrade power
            }
            Destroy(gameObject);    // Destroy the upgrader game object
        }
    }
}
