using UnityEngine;
using UnityEngine.UI;

public class OrderItem : MonoBehaviour
{
    public FoodType foodType;
    public float desiredCookPercentage;
    Color fillColor;
    public Image fillImage;
    public Image foodImage;

    public void Start()
    {

    }

    public void Initialize(float cookPercentage, FoodType foodType, Sprite foodSprite )
    {
        fillColor = FoodStuffs.GetColorFromPercent(cookPercentage);
        desiredCookPercentage = cookPercentage;

        fillImage.color = fillColor;
        fillImage.fillAmount = desiredCookPercentage;

        this.foodType = foodType;
        foodImage.sprite = foodSprite;
    }
}
