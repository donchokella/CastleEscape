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
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            if (player.HasKey(color))
            {
                OpenDoor();
                player.RemoveKey(color);
            }
        }
    }

    private void OpenDoor()
    {
        Collider[] doorCollider = GetComponents<Collider>();

        for (int i = 0; i < doorCollider.Length; i++)
        {
            Destroy(doorCollider[i]);
        }
    }
}
