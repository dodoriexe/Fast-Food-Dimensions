using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange;
    public KeyCode interactionKey;
    public Transform cameraObject;
    public bool holdingInteractKey;
    public Interactable lastInteract;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(interactionKey))
        {
            Interactable tempInt = lastInteract;
            if (tempInt != null)
            {
                tempInt.InteractLetGo();
            }
        }

        if (Physics.Raycast(cameraObject.position, cameraObject.transform.forward, out RaycastHit hitInfo, interactionRange))
        {
            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
            if(interactable != null)
            {
                lastInteract = interactable;
                interactable.LookAt();

                if (Input.GetKeyDown(interactionKey))
                {
                    interactable.Interact();
                    InteractText.NoTarget();
                }
            }
            else
            {
                InteractText.NoTarget();
            }
        }
        else
        {
            InteractText.NoTarget();
        }

        if(Input.GetKey(interactionKey))
        {
            holdingInteractKey = true;
        }


    }
}
