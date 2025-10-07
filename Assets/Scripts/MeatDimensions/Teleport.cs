using UnityEngine;

public class TeleportOnTouch : MonoBehaviour
{
    [Tooltip("The pickaxe pos")]
    public Transform teleportTarget;

    public bool portPlayer = false;

    private void FixedUpdate()
    {
        if(portPlayer)
        {
            portPlayer = false;

            GameManager.Instance.player.GetComponentInParent<Rigidbody>().linearVelocity = Vector3.zero;
            GameManager.Instance.player.transform.position = teleportTarget.transform.position;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Draggable>())
        {
            if(other.CompareTag("Pickaxe"))
            {
                Draggable draggable = other.GetComponent<Draggable>();

                other.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                other.transform.position = teleportTarget.transform.position;
            }
            else
            {
                Destroy(other.gameObject);
            }
            
        }
        if(other.CompareTag("Player"))
        {
            portPlayer = true;
        }
    }
}
