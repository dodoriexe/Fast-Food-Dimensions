using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameObject> customerPrefabs;
    public Transform customerSpawnPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnCustomer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCustomer()
    {
        GameObject customerPrefab = customerPrefabs[Random.Range(0, customerPrefabs.Count)];

        GameObject customer = Instantiate(customerPrefab, customerSpawnPoint.position, Quaternion.identity);
        Customer tempCustomer = customer.GetComponent<Customer>();

        customerPrefab.transform.position = new Vector3(customerSpawnPoint.position.x, customerSpawnPoint.position.y + tempCustomer.groundOffset, customerSpawnPoint.position.z);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
