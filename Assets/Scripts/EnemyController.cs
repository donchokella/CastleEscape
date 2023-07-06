using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    public int enemyLevel;
    public float attackAnimTime = 1f;
    public ParticleSystem DieP;
    public float speed = 2f;
    public WayPoints path;

    private TextMeshPro tmpComponent;
    private Animator animator;
    private Transform target;
    private int wayPointIndex = 0;


    private void Start()
    {
        animator = GetComponent<Animator>();
        tmpComponent = GetComponentInChildren<TextMeshPro>();

        if (tmpComponent != null)
        {
            tmpComponent.text = "Lv. " + enemyLevel;
        }

        target = path.points[0];
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            GetNextWayPoint();
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

    int directionModifier = 1;
    void GetNextWayPoint()
    {
        wayPointIndex += directionModifier;

        if (wayPointIndex >= path.points.Length || wayPointIndex < 0)
        {
            directionModifier *= -1;
            wayPointIndex += directionModifier;
        }

        target = path.points[wayPointIndex];
        transform.LookAt(target);
    }
}
