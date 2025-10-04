using UnityEngine;

public class DriveThruWindow : Interactable
{
    public GameObject playerObject;
    public bool isOpen = false;
    KeyCode interactionKey;

    public float openPositionX;
    public float closedPositionX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactionKey = playerObject.GetComponent<PlayerInteraction>().interactionKey;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void LookAt()
    {
        InteractText.Instance.ShowText(interactionPrompt);

        if (isOpen)
        {
            this.interactionPrompt = $"Press '{interactionKey}' to close the drive-thru window.";
        }
        else
        {
            this.interactionPrompt = $"Press '{interactionKey}' to open the drive-thru window.";
        }
    }

    public override void Interact()
    {
        MoveWindow();
        base.Interact();
    }

    public void MoveWindow()
    {
        switch(isOpen)
        {
            case true:

                // Is open, so close.
                transform.localPosition = new Vector3(closedPositionX, transform.localPosition.y, transform.localPosition.z);
                isOpen = false;

                break;
            case false:

                // Is closed, so open.
                transform.localPosition = new Vector3(openPositionX, transform.localPosition.y, transform.localPosition.z);
                isOpen = true;  

                break;
        }
    }
}
