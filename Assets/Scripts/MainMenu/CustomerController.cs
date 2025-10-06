using UnityEngine;
using System.Collections;

public class CustomerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed;
    public float followDistance;
    public float detectionRadius;
    public LayerMask customerLayer;

    private bool isStopped = false;
    private bool leaving = false;

    private Transform targetCustomer;

    void Update()
    {
        if (leaving)
        {
            MoveForward();
            return;
        }

        if (targetCustomer != null)
        {
            if (!targetCustomer.gameObject.activeInHierarchy)
            {
                targetCustomer = null;
                isStopped = false;
                return;
            }

            float currentDistance = Vector3.Distance(transform.position, targetCustomer.position);

            if (currentDistance > followDistance + 0.01f)
            {
                MoveForward();
            }
            else
            {
                isStopped = true;
            }

            return;
        }

        Transform found = FindCustomerAhead();
        if (found != null)
        {
            targetCustomer = found;

            float d = Vector3.Distance(transform.position, targetCustomer.position);
            if (d > followDistance + 0.01f)
                MoveForward();
            else
                isStopped = true;

            return;
        }

        if (!isStopped)
            MoveForward();
    }

    void MoveForward()
    {
        isStopped = false;
        Vector3 forwardTarget = transform.position + transform.forward * 10f;
        transform.position = Vector3.MoveTowards(transform.position, forwardTarget, moveSpeed * Time.deltaTime);
    }

    Transform FindCustomerAhead()
    {
        Vector3 center = transform.position + transform.forward * (detectionRadius * 0.5f);
        Collider[] hits = Physics.OverlapSphere(center, detectionRadius * 0.5f, customerLayer);

        Transform best = null;
        float bestDist = Mathf.Infinity;

        foreach (var col in hits)
        {
            if (col.transform == transform) continue;

            Vector3 dir = (col.transform.position - transform.position).normalized;
            float dot = Vector3.Dot(transform.forward, dir);

            if (dot < 0.3f) continue;

            float d = Vector3.Distance(transform.position, col.transform.position);
            if (d < bestDist)
            {
                bestDist = d;
                best = col.transform;
            }
        }

        return best;
    }

    public void StopAtDetector()
    {
        isStopped = true;
        StartCoroutine(LeaveAfterDelay());
    }

    private IEnumerator LeaveAfterDelay()
    {
        float waitTime = Random.Range(2f, 5f);
        yield return new WaitForSeconds(waitTime);

        leaving = true;
        isStopped = false;

        Destroy(gameObject, 3f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 center = transform.position + transform.forward * (detectionRadius * 0.5f);
        Gizmos.DrawWireSphere(center, detectionRadius * 0.5f);
    }
}
