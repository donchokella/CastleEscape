using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    public int enemyLevel;

    private GameObject enemyLvlObject;
    private TextMeshPro tmpComponent;

    private PlayerController player;
    private GameManager gameManager;

    private void Start()
    {
        // Find the PlayerController and GameManager component in the scene
        player = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();

        enemyLvlObject = transform.Find("EnemyLevel").gameObject;

        tmpComponent = enemyLvlObject.GetComponent<TextMeshPro>();

        if (tmpComponent != null)
        {
            tmpComponent.text = "Lv. " + enemyLevel;
        }
    }

    private void LateUpdate()
    {
        // Make the enemy level text face the camera
        tmpComponent.transform.LookAt(Camera.main.transform.forward + transform.position);
    }
}
