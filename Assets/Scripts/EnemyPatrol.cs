using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    // The patrol points
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;

    public float speedMultiplier = 2f;  // Multiplier for enemy movement speed
    private string pathStatus = "AtoC"; // Current path status

    private void Start()
    {
        LookAtPoint(pointB);    // Start by looking at point B
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "pointA")
        {
            LookAtPoint(pointB);    // Look at point B when reaching point A
            pathStatus = "AtoC";    // Set path status to A to C
        }

        if (other.name == "pointB")
        {
            if (pathStatus == "AtoC")
            {
                LookAtPoint(pointC);    // Look at point C when reaching point B in the A to C path
            }
            else
            {
                LookAtPoint(pointA);    // Look at point A when reaching point B in the C to A path
            }
        }

        if (other.name == "pointC")
        {
            pathStatus = "CtoA";    // Set path status to C to A
            LookAtPoint(pointB);    // Look at point B when reaching point C
        }
        MoveForvard();  // Move forward after changing direction
    }

    private void LookAtPoint(Transform targetPoint)
    {
        transform.LookAt(targetPoint);  // Rotate to face the target point
    }

    private void MoveForvard()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speedMultiplier;   // Move forward in the current facing direction with speed multiplier
    }
}
