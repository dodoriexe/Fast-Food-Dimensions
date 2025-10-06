using System;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using EasyTextEffects;

public class Dial : Interactable
{
    public TMPro.TextMeshPro DimensionDisplay;
    public GameObject doorObject;
    public bool isDoorOpen = true;
    
    private AudioSource _dialSoundPlayer;

    void Start()
    {
        _dialSoundPlayer = gameObject.GetComponent<AudioSource>();
        ChangeDimension();
    }

    public override void LookAt()
    {
        interactionPrompt = $"Press 'E' to change dimension." + Environment.NewLine + $"Current dimension: <color=#{GameManager.Instance.dimensions[GameManager.Instance.currentDimensionIndex].dimensionColor.ToHexString()}>{GameManager.Instance.dimensions[GameManager.Instance.currentDimensionIndex].dimensionName}";
        InteractText.Instance.ShowText(interactionPrompt);
        if (isDoorOpen)
        {
            interactionPrompt = $"Press 'E' to change dimension." + Environment.NewLine + $"Current dimension: <color=#{GameManager.Instance.dimensions[GameManager.Instance.currentDimensionIndex].dimensionColor.ToHexString()}>{GameManager.Instance.dimensions[GameManager.Instance.currentDimensionIndex].dimensionName}";
        }
        else
        {
            interactionPrompt = $"Loading Dimension";
        }
        InteractText.Instance.ShowText(interactionPrompt);
        
    }

    public override void Interact()
    {
        _dialSoundPlayer.Play();
        if (isDoorOpen)
        {
            CloseDoor();
            GameManager.Instance.currentDimensionIndex++;
            if (GameManager.Instance.currentDimensionIndex >= GameManager.Instance.dimensions.Count)
            {
                GameManager.Instance.currentDimensionIndex = 0;
            }
            InteractText.Instance.ShowText(interactionPrompt);
            base.Interact();
        }
        else
        {
            GameManager.Instance.currentDimensionIndex++;
            if (GameManager.Instance.currentDimensionIndex >= GameManager.Instance.dimensions.Count)
            {
                GameManager.Instance.currentDimensionIndex = 0;
            }
            InteractText.Instance.ShowText(interactionPrompt);
            UpdateDimensionLabel();
            ChangeDimension();
            base.Interact();
        }
    }

    void ChangeDimension()
    {
        GameManager.Instance.ChangeDimension(GameManager.Instance.currentDimensionIndex);
        UpdateDimensionLabel();
        base.Interact();
    }

    void CloseDoor()
    {
        ClosingScript closingScript = doorObject.GetComponent<ClosingScript>();
        if (closingScript != null)
        {
            StartCoroutine(CloseThenOpenCoroutine(closingScript));
            isDoorOpen = false;
        }
    }

    System.Collections.IEnumerator CloseThenOpenCoroutine(ClosingScript closingScript)
    {
        closingScript.Close();
        // Wait for the door to finish closing (adjust time as needed)
        yield return new WaitForSeconds(2.0f);
        UpdateDimensionLabel();
        yield return new WaitForSeconds(1.0f); // Wait a moment before changing dimension
        ChangeDimension();
        closingScript.Open();
        isDoorOpen = true;
    }

    void UpdateDimensionLabel() 
    {
        DimensionDisplay.text = GameManager.Instance.dimensions[GameManager.Instance.currentDimensionIndex].dimensionName;
        DimensionDisplay.color = GameManager.Instance.dimensions[GameManager.Instance.currentDimensionIndex].dimensionColor;
        DimensionDisplay.GetComponent<TextEffect>().Refresh();
    }

}
