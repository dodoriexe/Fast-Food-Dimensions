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

    public WindowTop TableTop;

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

    public static (int foods, int drinks, int items) GetOrderCounts(int score)
    {
        int drinks = 0;
        int items = 1;

        if (score >= 3) { drinks = 1; items = 2; }
        if (score >= 6) { drinks = 1; items = 3; }
        if (score >= 10) { drinks = 1; items = 4; }
        if (score >= 15) { drinks = 2; items = 5; }
        if (score >= 20) { drinks = 2; items = 6; }
        if (score >= 25) { drinks = 3; items = 7; }
        if (score >= 30) { drinks = 4; items = 8; }

        int foods = items - drinks;
        return (foods, drinks, items);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    internal List<OrderItemHolder> GenerateOrder()
    {
        List<OrderItemHolder> tempOrder = new List<OrderItemHolder>();
        // Generate Item Displays
        foodGenerateAmount = GetOrderCounts(happyCustomers).foods;
        drinkGenerateAmount = GetOrderCounts(happyCustomers).drinks;

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
        //float step = 0.01f;
        OrderItemHolder tempItem = foodOrderItems[Random.Range(0, foodOrderItems.Count)].toOrderItemHolder();
        tempItem.SetCookPercentage(Random.Range(0f, 1f));
        return tempItem;
    }

    internal OrderItemHolder GetRandomDrink()
    {
        if (drinkOrderItems == null || drinkOrderItems.Count == 0)
            return null;
        //float step = 0.01f;
        OrderItemHolder tempItem = drinkOrderItems[Random.Range(0, drinkOrderItems.Count)].toOrderItemHolder();
        tempItem.SetCookPercentage(Random.Range(0f, 1f));
        return tempItem;
    }
}