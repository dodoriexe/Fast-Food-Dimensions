using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class CustomerSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject customerPrefab;
    public Transform spawnPoint;
    public float minSpawnDelay = 2f;
    public float maxSpawnDelay = 6f;

    private bool spawningActive = true;
    private bool isOccupied = false;

    void Start()
    {
        BoxCollider box = GetComponent<BoxCollider>();
        box.isTrigger = true;

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (spawningActive)
        {
            yield return new WaitUntil(() => !isOccupied);

            SpawnCustomer();

            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);

            if (!SceneManager.GetActiveScene().isLoaded)
                spawningActive = false;
        }
    }

    void SpawnCustomer()
    {
        GameObject newCustomer = Instantiate(customerPrefab, spawnPoint.position, spawnPoint.rotation);

        Transform colorChild = newCustomer.transform.Find("Color");
        if (colorChild != null)
        {
            Renderer colorRenderer = colorChild.GetComponent<Renderer>();
            if (colorRenderer != null)
            {
                colorRenderer.material.color = GetRandomColor();
            }
        }
    }

    Color GetRandomColor()
    {
        float hue = Random.Range(0f, 1f);
        float saturation = Random.Range(0.5f, 1f);
        float value = Random.Range(0.6f, 1f);
        return Color.HSVToRGB(hue, saturation, value);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CustomerController>() != null)
        {
            isOccupied = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CustomerController>() != null)
        {
            isOccupied = false;
        }
    }

    void OnDisable()
    {
        spawningActive = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        BoxCollider box = GetComponent<BoxCollider>();
        if (box != null)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(box.center, box.size);
        }
    }
}
