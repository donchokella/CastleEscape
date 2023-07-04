using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Inventory.AllKeys requiredKeyColor;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Check if the player has the corresponding key to open the door
            if (Inventory.Instance.inventoryKeys.Contains(requiredKeyColor))
            {
                OpenDoor();
                // Remove the key from the player's inventory
                Inventory.Instance.RemoveKey(requiredKeyColor);
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
            doorCollider[i].enabled = false;
        }
    }
}
