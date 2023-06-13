using UnityEngine;

public class Sight : MonoBehaviour
{
    public LayerMask targetLayer;
    public float range = 10f;
    private Transform target;

    private void Update()
    {
        Collider[] targetsInSight = Physics.OverlapSphere(transform.position, range, targetLayer);

        if (targetsInSight.Length > 0)
        {
            float closestDistance = Mathf.Infinity;

            foreach (Collider targetCollider in targetsInSight)
            {
                float distanceToTarget = Vector3.Distance(transform.position, targetCollider.transform.position);
                if (distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget;
                    target = targetCollider.transform;
                }
            }
        }
        else
        {
            target = null;
        }
    }

    public Transform GetTarget()
    {
        return target;
    }
}
