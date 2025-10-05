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
        springJoint = GameManager.Instance.playerHands.GetComponent<SpringJoint>();
        springJoint.spring = 0f;
    }

    override public void Interact()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        // I had so much trouble with this fuck.
        // Spring joint is now on player.
        // Put Object's rigidbody into player's Spring Joint.

        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.linearDamping = 5f;
        //rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        beingHeld = true;
        springJoint.spring = 150f;    // Higher = stiffer
        springJoint.damper = 20f;     // Higher = less wobble
        springJoint.tolerance = 0.01f; // Lower = more precise

        //Debug.Log("Interacted with food item.");
        //GameManager.Instance.playerHands.transform.position = transform.position;
        rb.isKinematic = false;
        springJoint.connectedBody = rb;
        this.transform.SetParent(GameManager.Instance.player.transform);

    }

    override public void InteractLetGo()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        if (beingHeld)
        {
            beingHeld = false;
            springJoint.spring = 0f;
            springJoint.connectedBody = null;
            rb.linearDamping = 0f;
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
