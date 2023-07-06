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
    public AudioSource Kill, Die, CollectBook, CollectKey, UnlockedDoor, Victory;
    public ParticleSystem DieP, CollectBookP, VictoryP;

    public int playerLevel;
    private TextMeshPro tmpComponent;

    private List<EnemyController> enemies = new List<EnemyController>();


    public event System.Action OnVictory;
    public event System.Action OnLose;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerLevel = 3;

        tmpComponent = GetComponentInChildren<TextMeshPro>();

        if (tmpComponent != null)
        {
            tmpComponent.text = "Lv. " + playerLevel;
        }
    }

    private void LateUpdate()
    {
        // Make the player level text face the camera
        tmpComponent.transform.LookAt(Camera.main.transform.forward + transform.position);
        tmpComponent.transform.rotation = Quaternion.Euler(45, 0, 0);

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

            playerRB.constraints |= RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            animator.SetBool("isWalking", false);

            playerRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("VictoryArea"))
        {
            // Update game state when the player enters the victory area
            OnVictory?.Invoke();

            Victory.Play();
            VictoryP.Play();
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
                OnLose?.Invoke();


                Die.Play();
                DieP.Play();

                gameObject.GetComponent<Collider>().enabled = false;
                Destroy(gameObject, 2);

                moveSpeed = 0;
                // Dying Animation will be placed here
            }
        }
    }

    private IEnumerator AttackEnemy(EnemyController enemy)
    {
        if (enemy != null)
        {
            Kill.Play();
            enemy.DieP.Play();

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

        CollectBook.Play();
        CollectBookP.Play();
    }
}
