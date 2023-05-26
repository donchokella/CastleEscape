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

    public Animator animator;
    public float attackAnimTime = 1f;
    public ParticleSystem DieP;

    private void Start()
    {
        // Find the PlayerController and GameManager component in the scene
        player = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();

        animator = GetComponent<Animator>();

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
        tmpComponent.transform.rotation = Quaternion.Euler(45, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isWalking", false);   // If it is not walking, it is attacking
        }
    }
}
