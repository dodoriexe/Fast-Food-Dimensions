using UnityEngine;

public class VelocityText : MonoBehaviour
{
    public Rigidbody rb;
    TMPro.TextMeshProUGUI textMesh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMesh = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = $"Velocity: {rb.linearVelocity.magnitude}";
    }
}
