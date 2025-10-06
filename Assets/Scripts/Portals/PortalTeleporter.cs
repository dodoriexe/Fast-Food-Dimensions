using System.Collections;
using UnityEditor;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public Camera camera;
    public Transform player;
    public Transform receiver;

    public float exitOffset;

    private bool playerIsOverlapping = false;
    
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Improved teleportation logic to prevent immediate re-teleportation and wrong side landing
        if (playerIsOverlapping && Time.time - GameManager.Instance.lastTeleportTime > GameManager.Instance.teleportCooldown)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            float exitOffset = receiver.GetComponent<PortalTeleporter>().exitOffset;

            if (dotProduct < 0f)
            {
                // Calculate the relative position of the player to the portal in portal's local space
                Vector3 localPosition = transform.InverseTransformPoint(player.position);

                // Calculate the difference in rotation between the two portals
                Quaternion portalRotDiff = receiver.rotation * Quaternion.Inverse(transform.rotation);

                // Rotate the local position by the rotation difference
                Vector3 newLocalPosition = portalRotDiff * localPosition;

                // Offset to move player slightly forward from the receiver to avoid immediate re-trigger
                Vector3 offset = receiver.forward * exitOffset;

                // Set the player's new position in world space relative to the receiver, with offset
                StartCoroutine(DisableReceiverTemporarily(GameManager.Instance.teleportCooldown));
                player.GetComponent<Rigidbody>().isKinematic = true;

                player.position = receiver.TransformPoint(newLocalPosition) + offset;

                player.GetComponent<Rigidbody>().isKinematic = false;

                // Calculate the difference in Y rotation between the two portals
                float yRotDiff = receiver.eulerAngles.y - transform.eulerAngles.y + 180f;
                player.rotation = Quaternion.Euler(0f, player.eulerAngles.y + yRotDiff, 0f);
                
                // Swap player skybox to receiver camera skybox.
                gameManager.playerCamera.GetComponent<Skybox>().material = receiver.GetComponent<PortalTeleporter>()
                    .camera.GetComponent<Skybox>().material;

                Debug.Log("Teleported player through portal.");

                playerIsOverlapping = false;
                GameManager.Instance.lastTeleportTime = Time.time;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered portal trigger.");
        if (other.tag == "Player")
        {
            playerIsOverlapping = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Player exited portal trigger.");
        if (other.tag == "Player")
        {
            playerIsOverlapping = false;
        }
    }

    private IEnumerator DisableReceiverTemporarily(float duration)
    {
        Collider receiverCollider = receiver.GetComponent<Collider>();
        if (receiverCollider != null)
        {
            Debug.Log("Disabling receiver collider temporarily.");
            receiverCollider.enabled = false;
            yield return new WaitForSeconds(duration);
            receiverCollider.enabled = true;
        }
    }
}
