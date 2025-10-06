using UnityEngine;

public class SodaTop : MonoBehaviour
{
    public bool hasCup = false;
    public GameObject cupItem; // visual
    public Transform stockPoint;
    public GameObject cupPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void ClearCup()
    {
        hasCup = false;
        if (cupItem) cupItem.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EmptyCup"))
        {
            if (!hasCup)
            {
                hasCup = true;
                if (cupItem) cupItem.SetActive(true);
                Destroy(other.gameObject);
            }
            return;
        }
    }

    public void PourDrink(FoodType type, Sprite foodSprite)
    {
        if (!hasCup) return;
        
        Debug.Log($"Pouring drink of type: {type}");
        GameObject item = Instantiate(cupPrefab, stockPoint.position, cupPrefab.transform.rotation);
        FoodStuffs foodStuffs = item.GetComponent<FoodStuffs>();
        foodStuffs.Init();

        foodStuffs.foodType = type;
        foodStuffs.cookPercentage = 0;
        foodStuffs.orderItem = GameManager.Instance.orderItem;
        foodStuffs.foodSprite = foodSprite;

        ClearCup();
    }

}
