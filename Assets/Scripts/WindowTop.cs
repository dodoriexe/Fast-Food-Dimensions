using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WindowTop : MonoBehaviour
{
    public bool hasBag = false;
    public GameObject bagItem; // visual
    [SerializeField]
    private bool isColliding = false;

    public List<OrderItemHolder> bagContents = new List<OrderItemHolder>();

    void Start() { }

    void Update()
    {
        // If you rely on isColliding to prevent multiple trigger events,
        // don't reset it here every frame. Consider removing it or
        // resetting it in OnTriggerExit instead.
        isColliding = false;
    }

    public void ClearBag()
    {
        hasBag = false;
        if (bagItem) bagItem.SetActive(false);
        bagContents.Clear();
    }

    void OnTriggerEnter(Collider other)
    {
        if(isColliding) return; // consider removing or handling on exit
        isColliding = true;

        if (other.CompareTag("Paperbag"))
        {
            if (!hasBag)
            {
                hasBag = true;
                if (bagItem) bagItem.SetActive(true);
                Destroy(other.gameObject);
            }
            return;
        }

        var fs = other.GetComponent<FoodStuffs>();
        if (fs == null) return;

        if (hasBag)
        {
            bagContents.Add(new OrderItemHolder(fs.foodType, fs.cookPercentage, fs.orderItem, fs.foodSprite));
            Destroy(other.gameObject);

            // If bag exactly matches order count, evaluate
            if (bagContents.Count == GameManager.Instance.currentCustomer.order.Count)
            {
                OrderComplete();
            }
        }
    }

    // === Approach A: pairwise matching (defensive normalization) ===
    void OrderComplete()
    {
        Debug.Log(">>> OrderComplete() called. Bag count = " + bagContents.Count +
          ", order count = " + GameManager.Instance.currentCustomer.order.Count);
        bool orderMatches = true;

        // quick count check - fail fast if counts mismatch
        if (bagContents.Count != GameManager.Instance.currentCustomer.order.Count)
        {
            Debug.Log("OrderComplete: counts mismatch.");
            GameManager.Instance.currentCustomer.CompleteOrder(false);
            return;
        }

        // copy so matched items can't be reused
        List<OrderItemHolder> bagCopy = new List<OrderItemHolder>(bagContents);

        foreach (var requestedItem in GameManager.Instance.currentCustomer.order)
        {
            bool matched = false;
            for (int i = 0; i < bagCopy.Count; i++)
            {
                var bagEntry = bagCopy[i];

                // check same food type first   
                if (requestedItem.GetFoodType() != bagEntry.GetFoodType())
                    continue;

                // Normalize cook values defensively:
                // - requestedItem.GetCookPercentage() is expected to be 0..1 (normalized)
                // - bagEntry.GetCookPercentage() may be 1..100 (UI) -> divide by 100 if >1
                float requestedNormalized = requestedItem.GetCookPercentage();
                float bagNormalized = bagEntry.GetCookPercentage();
                if (bagNormalized > 1f) bagNormalized /= 100f;

                // Compare cook levels (using your helper). Assumes GetCookLevel returns discrete matching type.
                var requestedLevel = FoodStuffs.GetCookLevel(requestedNormalized);
                var bagLevel = FoodStuffs.GetCookLevel(bagNormalized);

                if (requestedLevel == bagLevel)
                {
                    matched = true;
                    bagCopy.RemoveAt(i);
                    break;
                }
            }

            if (!matched)
            {
                orderMatches = false;
                break;
            }
        }

        Debug.Log("OrderComplete: orderMatches = " + orderMatches);
        GameManager.Instance.currentCustomer.CompleteOrder(orderMatches);
    }

    // === Approach B (alternative): grouping counts by (foodType, cookLevel) — robust multiset comparison ===
    // Use this if you want a clearer, often faster, and easier-to-reason about correctness for duplicates.
    bool OrderMatchesByGrouping()
    {
        // Build a frequency dictionary for requested items
        var reqCounts = new Dictionary<(object foodType, int cookLevel), int>();
        foreach (var requestedItem in GameManager.Instance.currentCustomer.order)
        {
            float reqNormalized = requestedItem.GetCookPercentage();
            if (reqNormalized > 1f) reqNormalized /= 100f; // extra defensive
            int reqLevel = (int)FoodStuffs.GetCookLevel(reqNormalized);
            var key = (requestedItem.GetFoodType() as object, reqLevel);
            if (!reqCounts.ContainsKey(key)) reqCounts[key] = 0;
            reqCounts[key]++;
        }

        // Build frequency dictionary for bag items
        var bagCounts = new Dictionary<(object foodType, int cookLevel), int>();
        foreach (var bagEntry in bagContents)
        {
            float bagNormalized = bagEntry.GetCookPercentage();
            if (bagNormalized > 1f) bagNormalized /= 100f;
            int bagLevel = (int)FoodStuffs.GetCookLevel(bagNormalized);
            var key = (bagEntry.GetFoodType() as object, bagLevel);
            if (!bagCounts.ContainsKey(key)) bagCounts[key] = 0;
            bagCounts[key]++;
        }

        // Compare dictionaries
        if (reqCounts.Count != bagCounts.Count) return false; // quick check

        foreach (var kv in reqCounts)
        {
            if (!bagCounts.TryGetValue(kv.Key, out int bagCount) || bagCount != kv.Value)
                return false;
        }

        return true;
    }
}
