using UnityEngine;

public class TeleportOnTouch : MonoBehaviour
{
    [Tooltip("The pickaxe pos")]
    public Transform teleportTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Draggable>())
        {
            Draggable draggable = other.GetComponent<Draggable>();

            other.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            other.transform.position = teleportTarget.transform.position;
        }
    }
}
