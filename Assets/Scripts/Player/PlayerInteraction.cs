using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange;
    public KeyCode interactionKey;
    public Transform cameraObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(cameraObject.position, cameraObject.transform.forward, out RaycastHit hitInfo, interactionRange))
        {
            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
            if(interactable != null)
            {
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
    }
}
