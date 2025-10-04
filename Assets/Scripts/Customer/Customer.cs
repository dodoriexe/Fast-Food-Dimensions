using UnityEngine;

public class Customer : MonoBehaviour
{
    public CustomerType type;
    public float groundOffset;
    public float driveSpeed;

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
