using UnityEngine;

public class DriveStopper : MonoBehaviour
{
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
        Physics.Raycast(other.transform.position, Vector3.down, out RaycastHit hitInfo);
        if (other.tag == "Consumer")
        {

                other.GetComponentInParent<Customer>().AtDrive();

        }
    }
}