using UnityEngine;

public class Customer : MonoBehaviour
{
    public CustomerType type;
    public float groundOffset;
    public float driveSpeed;
    public AudioSource audioSource;
    public AudioClip[] driveClips; // Assign 2 clips in the Inspector

    public CustomerStates state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case CustomerStates.ToDrive:
                transform.position += Vector3.forward * driveSpeed;
                break;
            case CustomerStates.WaitingForOrder:

                break;
            case CustomerStates.Goodbye:

                break;
            default:
                break;
        }

    }

    

    public void AtDrive()
    {
        state = CustomerStates.WaitingForOrder;

        if (driveClips != null && driveClips.Length > 0 && audioSource != null)
        {
            int idx = Random.Range(0, driveClips.Length);
            audioSource.clip = driveClips[idx];
            audioSource.Play();
        }
    }
}

public enum CustomerType
{
    Car1,
    Alien,
    Skateboarder
}

public enum CustomerStates
{
    ToDrive,
    WaitingForOrder,
    Goodbye
}

