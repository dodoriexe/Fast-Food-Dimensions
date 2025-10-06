using System;
using UnityEngine;

public class ChillSpill : Interactable
{
    public GameObject chillPrefab;
    public Transform stockPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        Debug.Log("Interacted with " + this.gameObject.name);
        if (chillPrefab != null && stockPoint != null)
        {
            GameObject item = Instantiate(chillPrefab, stockPoint.position, chillPrefab.transform.rotation);
            Draggable draggable = item.GetComponent<Draggable>();
            draggable.Init();
        }
    }

    public override void LookAt()
    {
        KeyCode interactionKey = GameManager.Instance.player.GetComponent<PlayerInteraction>().interactionKey;
        interactionPrompt = $"Looks like there's been a chill spill..{Environment.NewLine}Care for a spin? '{interactionKey}'";
        InteractText.Instance.ShowText(interactionPrompt);
    }
}
