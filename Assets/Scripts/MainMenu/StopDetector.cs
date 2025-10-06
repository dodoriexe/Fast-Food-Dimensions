using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class StopDetector : MonoBehaviour
{
    private BoxCollider box;
    private readonly HashSet<CustomerController> stopped = new HashSet<CustomerController>();

    void Awake()
    {
        box = GetComponent<BoxCollider>();
        box.isTrigger = true;
    }

    void Update()
    {
        Vector3 center = transform.TransformPoint(box.center);
        Vector3 halfExtents = Vector3.Scale(box.size, transform.lossyScale) * 0.5f;

        Collider[] hits = Physics.OverlapBox(center, halfExtents, transform.rotation);
        HashSet<CustomerController> currentlyInside = new HashSet<CustomerController>();

        foreach (var hit in hits)
        {
            CustomerController c = hit.GetComponent<CustomerController>();
            if (c == null) continue;

            currentlyInside.Add(c);

            if (!stopped.Contains(c))
            {
                c.StopAtDetector();
                stopped.Add(c);
            }
        }

        stopped.RemoveWhere(c => c == null || !currentlyInside.Contains(c));
    }

    void OnDrawGizmosSelected()
    {
        if (box == null) box = GetComponent<BoxCollider>();
        Gizmos.color = new Color(1f, 0.5f, 0f, 0.4f);
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(box.center, box.size);
    }
}
