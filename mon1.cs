using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class gpt_mon1 : MonoBehaviour
{
    #region
    //public Transform player;
    //public float followDistance = 200f;
    //public float movementSpeed = 5f;
    //public float rotationSpeed = 2f;
    //public float patrolDistance = 150f;
    //public float waitTime = 2f;
    //private bool isFollowingPlayer = true;
    //private Vector3 destination;
    //private Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
    //private int currentDirectionIndex = 0;
    //private float elapsedTime = 0f;

    //private void Start()
    //{
    //    destination = transform.position;
    //}

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Wall"))
    //    {
    //        Vector3 contactPoint = collision.contacts[0].point;
    //        Vector3 normal = collision.contacts[0].normal;
    //        Vector3 direction = Vector3.Reflect(transform.forward, normal).normalized;

    //        // 벽의 방향으로 이동
    //        transform.position += direction * movementSpeed * Time.deltaTime;
    //    }
    //}


    //private void Update()
    //{
    //    if (isFollowingPlayer)
    //    {
    //        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
    //        if (distanceToPlayer <= followDistance)
    //        {
    //            // Rotate towards player
    //            Vector3 directionToPlayer = (player.position - transform.position).normalized;
    //            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
    //            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

    //            // Move towards player
    //            transform.position += transform.forward * movementSpeed * Time.deltaTime;
    //        }
    //        else
    //        {
    //            // If the monster is too far from the player, stop following and go back to the starting position
    //            isFollowingPlayer = false;
    //            destination = transform.position;
    //        }
    //    }
    //    else
    //    {
    //        elapsedTime += Time.deltaTime;
    //        if (elapsedTime > waitTime)
    //        {
    //            // Choose a random direction to patrol
    //            currentDirectionIndex = Random.Range(0, directions.Length);
    //            elapsedTime = 0f;
    //        }

    //        // Move towards the patrol destination
    //        destination = transform.position + directions[currentDirectionIndex] * patrolDistance * Time.deltaTime;
    //        transform.position = Vector3.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);

    //        // Check if the monster has gone too far from the starting position and destroy it if it has
    //        float distanceToStart = Vector3.Distance(transform.position, transform.parent.position);
    //        if (distanceToStart > followDistance)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}
    #endregion

    public Transform player;
    public Transform A;

    private NavMeshAgent navMeshAgent;
    private float idleTimer = 0f;
    private bool isChasing = false;
    private Vector3 targetPosition;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        targetPosition = A.position;
    }

    void Update()
    {
        if (IsPlayerInSight())
        {
            isChasing = true;
            idleTimer = 0f;
            //navMeshAgent.SetDestination(player.position);
        }
        else if (isChasing)
        {
            idleTimer = 0f;
            navMeshAgent.SetDestination(player.position);
        }
        else
        {
            idleTimer += Time.deltaTime;
            if (idleTimer > 3f)
            {
                targetPosition = GetRandomPosition();
                //navMeshAgent.SetDestination(targetPosition);
                idleTimer = 0f;
            }
        }
    }

    bool IsPlayerInSight()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                Vector3 direction = (hitCollider.transform.position - transform.position).normalized;
                if (Vector3.Dot(direction, transform.forward) > 0.5f)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, direction, out hit))
                    {
                        if (hit.collider.CompareTag("Player"))
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        float randomDistance = Random.Range(5f, 25f);
        Vector3 targetPosition = A.position + randomDirection * randomDistance;

        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(targetPosition, out navMeshHit, randomDistance, NavMesh.AllAreas);

        return navMeshHit.position;
    }
}