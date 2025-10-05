using System;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using EasyTextEffects;

public class Dial : Interactable
{
    public TMPro.TextMeshPro DimensionDisplay;

    void Start()
    {
        ChangeDimension();
    }

    public override void LookAt()
    {
        interactionPrompt = $"Press 'E' to change dimension." + Environment.NewLine + $"Current dimension: <color=#{GameManager.Instance.dimensions[GameManager.Instance.currentDimensionIndex].dimensionColor.ToHexString()}>{GameManager.Instance.dimensions[GameManager.Instance.currentDimensionIndex].dimensionName}";
        InteractText.Instance.ShowText(interactionPrompt);
        
    }

    public override void Interact()
    {
        GameManager.Instance.currentDimensionIndex++;
        if (GameManager.Instance.currentDimensionIndex >= GameManager.Instance.dimensions.Count)
        {
            GameManager.Instance.currentDimensionIndex = 0;
        }
        ChangeDimension();
        InteractText.Instance.ShowText(interactionPrompt);
        base.Interact();
    }

    void ChangeDimension()
    {
        GameManager.Instance.ChangeDimension(GameManager.Instance.currentDimensionIndex);
        UpdateDimensionLabel();
    }

    void UpdateDimensionLabel() 
    {
        DimensionDisplay.text = GameManager.Instance.dimensions[GameManager.Instance.currentDimensionIndex].dimensionName;
        DimensionDisplay.color = GameManager.Instance.dimensions[GameManager.Instance.currentDimensionIndex].dimensionColor;
        DimensionDisplay.GetComponent<TextEffect>().Refresh();
    }

}
