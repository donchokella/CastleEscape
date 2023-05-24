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

    private float attackAnimTime = 0.5f;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        enemyLvlObject = transform.Find("EnemyLevel").gameObject;

        tmpComponent = enemyLvlObject.GetComponent<TextMeshPro>();

        if (tmpComponent != null)
        {
            tmpComponent.text = "Lv. " + enemyLevel;
        }
    }
    private void LateUpdate()
    {
        tmpComponent.transform.LookAt(Camera.main.transform.forward + transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player.playerLevel > enemyLevel)
            {
                StartCoroutine(PlayerAttack());
            }
            else
            {
                Debug.Log("die");
                gameManager.UpdateGameStates(GameManager.GameState.Lose);
            }
        }
    }

    private IEnumerator PlayerAttack()
    {
        player.animator.SetBool("isAttacking", true);

        yield return new WaitForSeconds(attackAnimTime);

        player.animator.SetBool("isAttacking", false);
        Destroy(gameObject);
    }

}
