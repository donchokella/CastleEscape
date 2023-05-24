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
        GetComponent<Transform>().LookAt(pointB);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "pointA")
        {
            GetComponent<Transform>().LookAt(pointB);
            pathStatus = "AtoC";
        }

        if (other.name == "pointB")
        {
            if (pathStatus == "AtoC")
            {
                GetComponent<Transform>().LookAt(pointC);
            }
            else
            {
                GetComponent<Transform>().LookAt(pointA);
            }
        }

        if (other.name == "pointC")
        {
            pathStatus = "CtoA";
            GetComponent<Transform>().LookAt(pointB);
        }
        GetComponent<Rigidbody>().velocity = transform.forward * speedMultiplier;
    }
}
