using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyX : MonoBehaviour
{
    public Transform player;
    public Transform pointA;
    public float speed = 5f;
    public float visionRange = 10f;
    public float wallPassInterval = 50f;

    private bool isChasingPlayer = false;
    private float wallPassTimer = 0f;
    private bool isMovingToLeft = true;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        wallPassTimer += Time.deltaTime;

        if (wallPassTimer >= wallPassInterval && isChasingPlayer)
        {
            transform.position += transform.forward * 10f;
            wallPassTimer = 0f;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= visionRange)
        {
            transform.LookAt(player);

            if (distanceToPlayer <= 20f)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }

            isChasingPlayer = true;
            if (Vector3.Distance(this.transform.position, pointA.transform.position) > 20f)
            {
                transform.position = pointA.transform.position;
            }
        }
        else
        {
            if (transform.position.x <= pointA.position.x - 20f)
            {
                isMovingToLeft = false;
            }
            else if (transform.position.x >= pointA.position.x + 20f)
            {
                isMovingToLeft = true;
            }

            if (isMovingToLeft)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }

            isChasingPlayer = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pointA.position, 0.5f);
    }
}
