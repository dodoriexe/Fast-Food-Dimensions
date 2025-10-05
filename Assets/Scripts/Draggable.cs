using UnityEngine;

public class Draggable : Interactable
{
    public Vector3 desiredScale;

    public bool beingHeld;
    SpringJoint springJoint;
    
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.localScale != desiredScale)
        {
            this.transform.localScale = desiredScale;
        }

    }

    public void Init()
    {
        beingHeld = false;
        springJoint = gameObject.GetComponent<SpringJoint>();
    }

    override public void Interact()
    {
        beingHeld = true;
        springJoint.spring = 80f;
        //Debug.Log("Interacted with food item.");
        GameManager.Instance.playerHands.transform.position = transform.position;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        springJoint.connectedBody = GameManager.Instance.playerHands.GetComponent<Rigidbody>();
        this.transform.SetParent(GameManager.Instance.player.transform);

    }

    override public void InteractLetGo()
    {
        if (beingHeld)
        {
            beingHeld = false;
            springJoint.spring = 0f;
            springJoint.connectedBody = null;
            this.transform.SetParent(null);
            //gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    override public void LookAt()
    {
        KeyCode interactionKey = GameManager.Instance.player.GetComponent<PlayerInteraction>().interactionKey;
        interactionPrompt = $"Press '{interactionKey}' to pick up the item";
        InteractText.Instance.ShowText(interactionPrompt);
    }
}
