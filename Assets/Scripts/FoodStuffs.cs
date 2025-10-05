using UnityEngine;

public class FoodStuffs : Interactable
{
    public FoodType foodType;
    public float cookPercentage;
    public GameObject orderItem;
    public Sprite foodSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cookPercentage = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrillTop"))
        {
            cookPercentage += Time.deltaTime * 1f;
            cookPercentage = Mathf.Clamp(cookPercentage, 0f, 100f);
            Debug.Log($"Cook Percentage: {cookPercentage}%");
        }
    }

    public static CookLevel GetCookLevel(float cookPercent)
    {
        if (cookPercent < .25f)
        {
            return CookLevel.Raw;
        }
        else if (cookPercent < .66)
        {
            return CookLevel.Medium;
        }
        else if (cookPercent < 100f)
        {
            return CookLevel.WellDone;
        }
        else
        {
            return CookLevel.Burnt;
        }
    }

    public static Color GetColorFromPercent(float cookPercent)
    {
        Color tempColor = new Color(0, 0, 0);

        if (cookPercent < .25f)
        {
            tempColor = new Color(0.3254901960784314f, 1, 0);
        }
        else if (cookPercent < .66)
        {
            tempColor = new Color(1,1,0);
        }
        else if (cookPercent < 100f)
        {
            tempColor = new Color(0.8666666666666667f, 0.12549019607843137f, 0.12549019607843137f);
        }

        return tempColor;
    }
}

public enum CookLevel
{
    Raw,
    Medium,
    WellDone,
    Burnt
}

public enum FoodType
{
    // Food
    Burger,

    // Drinks
    DrinkGreenPee,
    DrinkSadDog,
    DrinkClassico,
    DrinkGlorpola,
    DrinkInk,
    DrinkMog,
    DrinkMisterFresh,
    DrinkElfStrike
}