using UnityEngine;
using UnityEngine.UI;

public class FoodStuffs : Draggable
{
    public FoodType foodType;
    public float cookPercentage;
    public GameObject orderItem;
    public Sprite foodSprite;

    public Transform canvasObject;
    public Image cookImage;
    public GameObject rawText;

    public float canvasOffset = 0.5f;


    public GameObject connectObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
        cookPercentage = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.localScale != desiredScale)
        {
            this.transform.localScale = new Vector3(desiredScale.x, desiredScale.y, desiredScale.z);
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GrillTop"))
        {
            ShowCookImage();

            cookPercentage += Time.deltaTime * 1f;
            cookPercentage = Mathf.Clamp(cookPercentage, 0f, 100f);
            Debug.Log($"Cook Percentage: {cookPercentage}%");
        }

        if(other.CompareTag("WindowTop"))
        {
            // Bag Item
        }
    }

    private void ShowCookImage()
    {
        if (cookImage == null || canvasObject == null || GameManager.Instance.playerCamera == null)
            return;

        cookImage.gameObject.SetActive(true);
        cookImage.fillAmount = cookPercentage / 100f;
        cookImage.color = GetColorFromPercent(cookPercentage / 100f);

        Camera cam = GameManager.Instance.playerCamera;
        Vector3 camPos = cam.transform.position;
        Vector3 foodPos = transform.position;

        Vector3 dirToCam = (camPos - foodPos).normalized;

        float distToCam = Vector3.Distance(camPos, foodPos);
        float minDistFromCam = 0.6f;   // prevents UI from moving inside the player
        float verticalOffset = 0.2f;   // lifts UI above the food a little

        Vector3 desiredPos;

        if (distToCam <= minDistFromCam)
        {
            // If the camera is too close, push the canvas in front of the camera toward the food
            desiredPos = camPos - dirToCam * minDistFromCam;
        }
        else
        {
            // Normal placement: a fixed offset in front of the food, toward camera
            float useOffset = Mathf.Min(canvasOffset, distToCam - minDistFromCam);
            desiredPos = foodPos + dirToCam * useOffset;
        }

        // Apply vertical lift
        desiredPos += Vector3.up * verticalOffset;
        canvasObject.position = desiredPos;

        // Face Camera
        Vector3 lookDir = canvasObject.position - camPos;
        lookDir.y = 0f; // keep it upright; remove this line if you want full tilt toward camera
        if (lookDir.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir.normalized, Vector3.up);
            canvasObject.rotation = targetRot;
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
        if(cookPercent == 0f)
        {
            return CookLevel.Raw;
        }
        if (cookPercent < .25f)
        {
            return CookLevel.Rare;
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
    Rare,
    Medium,
    WellDone,
    Burnt
}

public enum FoodType
{
    // Food
    Burger,

    // Ingredients/Food
    Tomato,
    Lettuce,
    Pickle,

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