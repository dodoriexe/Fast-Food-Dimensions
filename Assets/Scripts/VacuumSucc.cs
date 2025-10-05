using UnityEngine;

public class VacuumSucc : MonoBehaviour
{
    public GameObject succPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Draggable>())
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null && succPoint != null)
            {
                Vector3 direction = (succPoint.transform.position - other.transform.position).normalized;
                rb.AddForce(direction * 10f, ForceMode.Impulse);
            }
        }
    }
}
