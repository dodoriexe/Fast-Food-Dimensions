using UnityEngine;

public class SuccTeleport : MonoBehaviour
{
    public GameObject teleportPoint;
    private AudioSource _succSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _succSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Draggable>() && !(other.CompareTag("Brown Bug") || other.CompareTag("Red Bug") || other.CompareTag("Green Bug")))
        {
            Draggable draggable = other.GetComponent<Draggable>();

            other.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.position = teleportPoint.transform.position;
            other.GetComponent<Rigidbody>().isKinematic = false;
            
            _succSound.Play();
        }
    }
}
