using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrader : MonoBehaviour
{
    public int upgradePower;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                Upgrader upgrader = GetComponent<Upgrader>();

                if (upgrader != null)
                {
                    int upgradePower = upgrader.upgradePower;
                    playerController.IncreasePlayerLevel(upgradePower);
                }
            }
            Destroy(gameObject);
        }
    }
}
