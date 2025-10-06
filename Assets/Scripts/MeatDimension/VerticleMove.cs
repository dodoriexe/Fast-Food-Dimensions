using UnityEngine;

public class VerticalMove : MonoBehaviour
{
    public float amplitude = 1f;  // How far up and down it moves
    public float speed = 1f;      // How fast it moves

    private Vector3 startPosition;

    void Start()
    {
        // Store the starting position
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate vertical offset
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * amplitude;

        // Apply movement along world Y-axis
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
