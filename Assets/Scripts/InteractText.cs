using UnityEngine;

public class InteractText : MonoBehaviour
{
    public static InteractText Instance;
    public TMPro.TextMeshProUGUI interactText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowText(string text)
    {
        interactText.text = text;
    }

    public static void NoTarget()
    {
        if(Instance.interactText.gameObject.activeSelf)
        {
            Instance.interactText.text = string.Empty;
        }
    }
}
