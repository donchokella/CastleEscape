using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;

    public float speedMultiplier = 2f;
    private string pathStatus = "AtoC";

    private void Start()
    {
        LookAtPoint(pointB);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "pointA")
        {
            LookAtPoint(pointB);
            pathStatus = "AtoC";
        }

        if (other.name == "pointB")
        {
            if (pathStatus == "AtoC")
            {
                LookAtPoint(pointC);
            }
            else
            {
                LookAtPoint(pointA);
            }
        }

        if (other.name == "pointC")
        {
            pathStatus = "CtoA";
            LookAtPoint(pointB);
        }
        MoveForvard();
    }

    private void LookAtPoint(Transform targetPoint)
    {
        transform.LookAt(targetPoint);
    }

    private void MoveForvard()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speedMultiplier;
    }
}
