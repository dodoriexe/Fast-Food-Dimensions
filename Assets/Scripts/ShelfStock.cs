using System;
using UnityEngine;

public class ShelfStock : Interactable
{

    public GameObject stockedPrefab;
    public Transform stockPoint;
    public string stockedItemName;

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
        if (stockedPrefab != null && stockPoint != null)
        {
            GameObject item = Instantiate(stockedPrefab, stockPoint.position, stockedPrefab.transform.rotation);
            Draggable draggable = item.GetComponent<Draggable>();
            draggable.Init();
        }
    }

    public override void LookAt()
    {
        KeyCode interactionKey = GameManager.Instance.player.GetComponent<PlayerInteraction>().interactionKey;
        interactionPrompt = $"Press '{interactionKey}' to pick a {stockedItemName} from the shelf!";
        InteractText.Instance.ShowText(interactionPrompt);
    }
}
