using System;
using Unity.VisualScripting;
using UnityEngine;

public class SodaButton : Interactable
{
    public string sodaName;
    public Color sodaColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void LookAt()
    {
        interactionPrompt = $"Press 'E' to pour " + Environment.NewLine + $"<color=#{sodaColor.ToHexString()}>{sodaName} !";
        InteractText.Instance.ShowText(interactionPrompt);

    }

    public override void Interact()
    {

        InteractText.Instance.ShowText(interactionPrompt);
        base.Interact();
    }
}
