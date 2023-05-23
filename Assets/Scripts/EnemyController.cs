using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    public int enemyLevel;

    GameObject enemyLvlObject;
    TextMeshPro tmpComponent;

    PlayerController player;

    GameManager gameManager;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        enemyLvlObject = transform.Find("EnemyLevel").gameObject;

        tmpComponent = enemyLvlObject.GetComponent<TextMeshPro>();

        if (tmpComponent != null)
        {
            Debug.Log("girdi");
            tmpComponent.text = "Lv. " + enemyLevel;
        }
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
