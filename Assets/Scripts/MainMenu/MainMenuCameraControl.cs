using UnityEngine;

public class MainMenuCameraControl : MonoBehaviour
{
    public float parallaxStrength = 0.1f; // camera movement
    public float rotationStrength = 0.5f; // camera rotate
    public float smoothSpeed = 5f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 normalizedMousePos = new Vector2(
            (mousePos.x / Screen.width) + 0.5f,
            (mousePos.y / Screen.height) - 0.5f
        ) * 2;

        Vector3 targetPosition = initialPosition + new Vector3(-normalizedMousePos.x, -normalizedMousePos.y, 0) * parallaxStrength;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(-normalizedMousePos.y * rotationStrength, normalizedMousePos.x * rotationStrength, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smoothSpeed);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * smoothSpeed);
    }
}