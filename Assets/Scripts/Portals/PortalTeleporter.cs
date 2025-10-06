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
                // Disable Teleporter Temporarily.
                StartCoroutine(DisableReceiverTemporarily(GameManager.Instance.teleportCooldown));
                
                // Teleport player to receiver.
                Vector3 offset = (receiver.forward * 0.5f) - new Vector3(0, receiver.GetComponent<BoxCollider>().size.y * 2, 0);
                player.position = receiver.position + offset;
                
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
