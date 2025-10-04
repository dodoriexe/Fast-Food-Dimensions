using UnityEngine;

public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public string interactionPrompt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted with " + this.gameObject.name);
    }

    public virtual void LookAt()
    {
        
    }
}
