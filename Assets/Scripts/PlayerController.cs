using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody playerRB;

    [SerializeField]
    private FloatingJoystick joystick;

    [SerializeField]
    private float moveSpeed;
    private Animator animator;

    public int playerLevel;
    GameObject playerLvlObject;
    TextMeshPro tmpComponent;

    GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
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

    private void FixedUpdate()
    {
        playerRB.velocity = new Vector3(joystick.Horizontal * moveSpeed, playerRB.velocity.y, joystick.Vertical * moveSpeed);

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(playerRB.velocity);

            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKey("x"))
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("VictoryArea"))
        {
            gameManager.UpdateGameStates(GameManager.GameState.Victory);
        }
    }

    public void IncreasePlayerLevel(int upgradePower)
    {
        playerLevel += upgradePower;

        tmpComponent.text = "Lv. " + playerLevel;
    }
}
