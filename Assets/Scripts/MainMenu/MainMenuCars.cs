using UnityEngine;

public class MainMenuCars : MonoBehaviour
{
    public GameObject customerPrefab;
    public float moveSpeed;

    private void start()
    {
        spawnCustomer();
    }

    void spawnCustomer()
    {
        GameObject customer = Instantiate(customerPrefab, transform.position, Quaternion.identity);
        StartCoroutine(MoveCustomerForward(customer));
    }

    System.Collections.IEnumerator MoveCustomerForward(GameObject customer)
    {
        while (true)
        {
            customer.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            if (customer.GetComponent<Collider>().bounds.Intersects(GetComponent<Collider>().bounds))
            {
                break;
            }
        }

        yield return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
