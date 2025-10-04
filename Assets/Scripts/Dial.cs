using System;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using EasyTextEffects;

public class Dial : Interactable
{
    int currentDimensionIndex;
    public TMPro.TextMeshPro DimensionDisplay;

    void Start()
    {
        currentDimensionIndex = 0;
        UpdateDimensionLabel();
    }

    public override void LookAt()
    {
        interactionPrompt = $"Press 'E' to change dimension." + Environment.NewLine + $"Current dimension: <color=#{GameManager.Instance.dimensions[currentDimensionIndex].dimensionColor.ToHexString()}>{GameManager.Instance.dimensions[currentDimensionIndex].dimensionName}";
        InteractText.Instance.ShowText(interactionPrompt);
        
    }

    public override void Interact()
    {
        currentDimensionIndex++;
        if (currentDimensionIndex >= GameManager.Instance.dimensions.Count)
        {
            currentDimensionIndex = 0;
        }
        UpdateDimensionLabel();
        InteractText.Instance.ShowText(interactionPrompt);
        base.Interact();
    }

    void UpdateDimensionLabel()
    {
        DimensionDisplay.text = GameManager.Instance.dimensions[currentDimensionIndex].dimensionName;
        DimensionDisplay.color = GameManager.Instance.dimensions[currentDimensionIndex].dimensionColor;
        DimensionDisplay.GetComponent<TextEffect>().Refresh();
    }

}
