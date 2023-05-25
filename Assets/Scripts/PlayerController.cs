using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private float moveSpeed;
    
    public Animator animator;
    public float attackAnimTime = 0.5f;

    public int playerLevel;
    private GameObject playerLvlObject;
    private TextMeshPro tmpComponent;

    private GameManager gameManager;
    private List<string> keys = new List<string>();

    private List<EnemyController> enemies = new List<EnemyController>();

    void Start()
    {
        // Find the GameManager component in the scene
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        animator = GetComponent<Animator>();
        playerLevel = 3;

        playerLvlObject = transform.Find("PlayerLevel").gameObject;
        tmpComponent = playerLvlObject.GetComponent<TextMeshPro>();

        if (tmpComponent != null)
        {
            tmpComponent.text = "Lv. " + playerLevel;
        }
    }

    private void LateUpdate()
    {
        // Make the player level text face the camera
        tmpComponent.transform.LookAt(Camera.main.transform.forward + transform.position);
    }

    private void FixedUpdate()
    {
        // Move the player based on joystick input
        playerRB.velocity = new Vector3(joystick.Horizontal * moveSpeed, playerRB.velocity.y, joystick.Vertical * moveSpeed);

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            // Rotate the player to face the movement direction
            transform.rotation = Quaternion.LookRotation(playerRB.velocity);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("VictoryArea"))
        {
            // Update game state when the player enters the victory area
            gameManager.UpdateGameStates(GameManager.GameState.Victory);
        }

        if (other.CompareTag("Enemy"))
        {
            // Check if the player level is higher than the enemy level
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (playerLevel > enemy.enemyLevel)
            {
                StartCoroutine(AttackEnemy(enemy));
            }
            else
            {
                // Player is defeated if the enemy level is equal or higher
                Debug.Log("You are defeated");
                gameManager.UpdateGameStates(GameManager.GameState.Lose);
            }
        }
    }

    private IEnumerator AttackEnemy(EnemyController enemy)
    {
        if (enemy != null)
        {
            // Perform attack animation and destroy the enemy after a delay
            animator.SetBool("isAttacking", true);
            yield return new WaitForSeconds(attackAnimTime);
            animator.SetBool("isAttacking", false);

            enemies.Remove(enemy);
            Destroy(enemy.gameObject);
        }
    }

    public void IncreasePlayerLevel(int upgradePower)
    {
        // Increase player level and update the displayed level text
        playerLevel += upgradePower;
        tmpComponent.text = "Lv. " + playerLevel;
    }

    public void AddKey(string color)
    {
        // Add a key to the player's inventory
        keys.Add(color);
    }

    public bool HasKey(string color)
    {
        // Check if the player has a specific key in the inventory
        return keys.Contains(color);
    }

    public void RemoveKey(string color)
    {
        // Remove a key from the player's inventory
        keys.Remove(color);
    }
}
