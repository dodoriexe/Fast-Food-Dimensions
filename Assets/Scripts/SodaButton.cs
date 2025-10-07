using System;
using Unity.VisualScripting;
using UnityEngine;

public class SodaButton : Interactable
{
    public string sodaName;
    public Color sodaColor;
    public FoodType sodaType;
    public Sprite foodSprite;

    public override void LookAt()
    {
        interactionPrompt = $"Press 'E' to pour " + Environment.NewLine + $"<color=#{sodaColor.ToHexString()}>{sodaName} !";
        InteractText.Instance.ShowText(interactionPrompt);

    }

    public override void Interact()
    {
        GameManager.Instance.SodaTop.PourDrink(sodaType, foodSprite);
        base.Interact();
    }
}
