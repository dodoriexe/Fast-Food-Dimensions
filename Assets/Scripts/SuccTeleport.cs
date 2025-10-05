using UnityEngine;

public class SuccTeleport : MonoBehaviour
{
    public GameObject teleportPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Draggable>())
        {
            other.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.position = teleportPoint.transform.position;
            other.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
