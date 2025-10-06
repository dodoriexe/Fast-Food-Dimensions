using UnityEngine;

public class SodaTop : MonoBehaviour
{
    public bool hasCup = false;
    public GameObject cupItem; // visual

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void ClearCup()
    {
        hasCup = false;
        if (cupItem) cupItem.SetActive(false);
    }

}
