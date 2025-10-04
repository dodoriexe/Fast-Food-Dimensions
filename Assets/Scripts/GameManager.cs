using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameObject> customerPrefabs;
    public Transform customerSpawnPoint;

    public List<Dimension> dimensions;
    public List<Portal> dimensionPortals;
    public int currentDimensionIndex;

    public float teleportCooldown = 0.25f;
    public float lastTeleportTime = -1f;

    public GameObject kitchenCam;
    public GameObject portalFrame;
    public GameObject kitchenCollider;

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

        currentDimensionIndex = 0;
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

    public void ChangeDimension(int newDimensionIndex)
    {
        if (newDimensionIndex < 0 || newDimensionIndex >= dimensions.Count)
        {
            Debug.LogError("Invalid dimension index");
            return;
        }
        currentDimensionIndex = newDimensionIndex;

        kitchenCollider.GetComponent<PortalTeleporter>().receiver = dimensionPortals[currentDimensionIndex].portalCollider.transform;
        portalFrame.GetComponent<MeshRenderer>().material = dimensionPortals[currentDimensionIndex].thisTargetMaterial;

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}