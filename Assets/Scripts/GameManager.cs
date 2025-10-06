using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public GameObject playerHands;
    public Camera playerCamera;
    public List<GameObject> customerPrefabs;
    public Transform customerSpawnPoint;

    public List<Dimension> dimensions;
    public List<Portal> dimensionPortals;
    public int currentDimensionIndex;

    public List<FoodStuffs> foodOrderItems;
    public List<FoodStuffs> drinkOrderItems;

    public Customer currentCustomer;

    public float teleportCooldown = 0.25f;
    public float lastTeleportTime = -1f;

    public GameObject kitchenCam;
    public GameObject portalFrame;
    public GameObject kitchenCollider;

    public GameObject orderSignHolder;

    public int foodGenerateAmount;
    public int drinkGenerateAmount;

    public int happyCustomers; // High Score


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
        currentCustomer = customer.GetComponent<Customer>();

        customerPrefab.transform.position = new Vector3(customerSpawnPoint.position.x, customerSpawnPoint.position.y + currentCustomer.groundOffset, customerSpawnPoint.position.z);
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

    internal List<OrderItemHolder> GenerateOrder()
    {
        List<OrderItemHolder> tempOrder = new List<OrderItemHolder>();
        // Generate Item Displays

        // Food
        for (int i = 0; i < foodGenerateAmount; i++)
        {
            tempOrder.Add(GetRandomFood());

        }

        // Drink
        for (int i = 0; i < drinkGenerateAmount; i++)
        {
            tempOrder.Add(GetRandomDrink());
        }



        // Generate an order with items that have random cook percentages. ( Raw, Medium, WellDone )

        return tempOrder;

    }
    internal OrderItemHolder GetRandomFood()
    {
        if (foodOrderItems == null || foodOrderItems.Count == 0)
            return null;
        float step = 0.01f;
        OrderItemHolder tempItem = foodOrderItems[Random.Range(0, foodOrderItems.Count)].toOrderItemHolder();
        tempItem.SetCookPercentage(Mathf.Round(Random.Range(0, 1f) / step) * step);
        return tempItem;
    }

    internal OrderItemHolder GetRandomDrink()
    {
        if (drinkOrderItems == null || drinkOrderItems.Count == 0)
            return null;
        float step = 0.01f;
        OrderItemHolder tempItem = foodOrderItems[Random.Range(0, foodOrderItems.Count)].toOrderItemHolder();
        tempItem.SetCookPercentage(Mathf.Round(Random.Range(0, 1f) / step) * step);
        return drinkOrderItems[Random.Range(0, drinkOrderItems.Count)].toOrderItemHolder();
    }
}