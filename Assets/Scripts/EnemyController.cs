using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int enemyLevel;

    PlayerController player;

    GameManager gameManager;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player.playerLevel > enemyLevel)
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("die");
                gameManager.UpdateGameStates(GameManager.GameState.Lose);
            }
        }
    }

}
