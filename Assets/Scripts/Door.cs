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
        Destroy(doorCollider[0]);
        Destroy(doorCollider[1]);
    }
}
