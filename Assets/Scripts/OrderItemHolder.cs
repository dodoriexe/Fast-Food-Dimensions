using System;
using UnityEngine;

[System.Serializable]
public class OrderItemHolder
{
    public FoodType foodType;
    public float cookPercentage;
    public GameObject orderItem;
    public Sprite foodSprite;

    public OrderItemHolder(FoodType foodType, float cookPercentage, GameObject orderItem, Sprite foodSprite)
    {
        this.foodType = foodType;
        this.cookPercentage = cookPercentage;
        this.orderItem = orderItem;
        this.foodSprite = foodSprite;
    }

    public FoodType GetFoodType() { return foodType; }
    public float GetCookPercentage() { return cookPercentage; }

    internal void SetCookPercentage(float cookPercentage)
    {
        this.cookPercentage = cookPercentage;
    }

    public Sprite GetFoodSprite() { return foodSprite; }

    internal void SetFoodSprite(Sprite foodSprite)
    {
        this.foodSprite = foodSprite;
    }

    public GameObject GetOrderItem() { return orderItem; }
    internal void SetOrderItem(GameObject orderItem)
    {
        this.orderItem = orderItem;
    }
}
