using UnityEngine;

public class DriveExterminator : MonoBehaviour
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
        if(other.CompareTag("Consumer"))
        {
            if (other.GetComponentInParent<Customer>().unhappy)
            {
                GameManager.Instance.GameOver("Something on this one's order wasn't right though!");
                return;
            }
            else
            {
                Destroy(other.gameObject);
                GameManager.Instance.SpawnCustomer();
            }

        }
    }
}
