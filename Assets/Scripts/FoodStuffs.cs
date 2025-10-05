using UnityEngine;

public class FoodStuffs : Interactable
{
    public FoodType foodType;
    public float cookPercentage;
    public GameObject orderItem;
    public Sprite foodSprite;
    SpringJoint springJoint;
    public bool beingHeld;

    public GameObject connectObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.springJoint = gameObject.GetComponent<SpringJoint>();
        cookPercentage = 0f;
        beingHeld = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.localScale != new Vector3(0.4f,0.4f,0.4f))
        {
            this.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
    }

    override public void Interact()
    {
        beingHeld = true;
        springJoint.spring = 50f;
        //Debug.Log("Interacted with food item.");
        GameManager.Instance.playerHands.transform.position = transform.position;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        springJoint.connectedBody = GameManager.Instance.playerHands.GetComponent<Rigidbody>();
        this.transform.SetParent(GameManager.Instance.player.transform);
        //gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    override public void InteractLetGo()
    {
        if(beingHeld)
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

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GrillTop"))
        {
            cookPercentage += Time.deltaTime * 1f;
            cookPercentage = Mathf.Clamp(cookPercentage, 0f, 100f);
            Debug.Log($"Cook Percentage: {cookPercentage}%");
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        
        if (other.collider.CompareTag("GrillTop"))
        {
            // Sizzle Noise
        }
    }

    public static CookLevel GetCookLevel(float cookPercent)
    {
        if (cookPercent < .25f)
        {
            return CookLevel.Raw;
        }
        else if (cookPercent < .66)
        {
            return CookLevel.Medium;
        }
        else if (cookPercent < 100f)
        {
            return CookLevel.WellDone;
        }
        else
        {
            return CookLevel.Burnt;
        }
    }

    public static Color GetColorFromPercent(float cookPercent)
    {
        Color tempColor = new Color(0, 0, 0);

        if (cookPercent < .25f)
        {
            tempColor = new Color(0.3254901960784314f, 1, 0);
        }
        else if (cookPercent < .66)
        {
            tempColor = new Color(1,1,0);
        }
        else if (cookPercent < 100f)
        {
            tempColor = new Color(0.8666666666666667f, 0.12549019607843137f, 0.12549019607843137f);
        }

        return tempColor;
    }
}

public enum CookLevel
{
    Raw,
    Medium,
    WellDone,
    Burnt
}

public enum FoodType
{
    // Food
    Burger,

    // Drinks
    DrinkGreenPee,
    DrinkSadDog,
    DrinkClassico,
    DrinkGlorpola,
    DrinkInk,
    DrinkMog,
    DrinkMisterFresh,
    DrinkElfStrike
}