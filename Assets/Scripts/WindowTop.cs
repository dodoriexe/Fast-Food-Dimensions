using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WindowTop : MonoBehaviour
{
    public bool hasBag = false;
    public GameObject bagItem;

    public List<FoodStuffs> bagContents = new List<FoodStuffs>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ClearBag()
    {
        hasBag = false;
        bagItem.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Paperbag"))
        {
            hasBag = true;
            bagItem.SetActive(true);
            Destroy(other.gameObject);
        }

        if(other.GetComponent<FoodStuffs>())
        {
            // Place into bag, if bag exists
            if(hasBag)
            {
                FoodStuffs fs = other.GetComponent<FoodStuffs>();
                bagContents.Add(fs);
                Destroy(other.gameObject);
            }
        }

        if (bagContents.Count == GameManager.Instance.currentCustomer.order.Count)
        {
            OrderComplete();
        }
    }
    
    void OrderComplete()
    {
        // Check if bag contents match order
        bool orderMatches = true;

        foreach (var item in GameManager.Instance.currentCustomer.order)
        {
            bool matchesCookPercentage = false;

            bool itemFound = false;
            foreach (var bagItem in bagContents)
            {
                if (item.foodType == bagItem.foodType && bagItem.cookPercentage >= 80f)
                {
                    matchesCookPercentage = FoodStuffs.GetCookLevel(item.cookPercentage) == FoodStuffs.GetCookLevel(bagItem.cookPercentage);
                    if (matchesCookPercentage)
                    {
                        itemFound = true;
                        break;
                    }
                }
                if (!itemFound)
                {
                    orderMatches = false;
                    break;
                }
            }
            GameManager.Instance.currentCustomer.CompleteOrder(orderMatches);
        }
    }
}
