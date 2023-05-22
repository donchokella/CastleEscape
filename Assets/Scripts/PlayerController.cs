using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody playerRB;

    [SerializeField]
    private FloatingJoystick joystick;

    [SerializeField]
    private float moveSpeed;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        playerRB.velocity = new Vector3(joystick.Horizontal * moveSpeed, playerRB.velocity.y, joystick.Vertical * moveSpeed);

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(playerRB.velocity);

            // animator bool' u gelecek
        }
        else
        {
            // animator
        }
    }
}
