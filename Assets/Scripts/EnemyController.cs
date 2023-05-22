using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int enemyLevel;

    PlayerController player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player.playerLevel > enemyLevel)
            {
                Debug.Log("kill");
            }
            else
            {
                Debug.Log("die");
            }
        }
    }

}
