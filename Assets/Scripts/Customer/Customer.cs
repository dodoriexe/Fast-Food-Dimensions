using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public CustomerType type;
    public float groundOffset;
    public float driveSpeed;
    public AudioSource audioSource;
    public AudioClip[] driveClips; // Assign 2 clips in the Inspector

    public CustomerStates state;

    public List<FoodStuffs> order;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<FoodStuffs> myOrder = (GameManager.Instance.GenerateOrder());
        order = myOrder;

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case CustomerStates.ToDrive:
                transform.position += Vector3.forward * driveSpeed;
                break;
            case CustomerStates.Ordering:

                StartCoroutine(ShowOrderWithDelay(0.5f));
                state = CustomerStates.WaitingForOrder;

                break;
            case CustomerStates.WaitingForOrder:

                break;
            case CustomerStates.Goodbye:

                break;
            default:
                break;
        }
    }
    private System.Collections.IEnumerator ShowOrderWithDelay(float delay)
    {
        foreach (var item in order)
        {
            if (item != null && item.orderItem != null)
            {
                GameObject orderObj = Instantiate(item.orderItem);
                float randomCook = Random.Range(0f, 1f);
                orderObj.GetComponent<OrderItem>().Initialize(randomCook, item.foodType, item.foodSprite);

                orderObj.transform.SetParent(GameManager.Instance.orderSignHolder.transform, false);
                PlayDriveSound();
                yield return new WaitForSeconds(delay);
            }
        }
    }

    public void AtDrive()
    {
        state = CustomerStates.Ordering;

    }

    public void PlayDriveSound()
    {
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
    Ordering,
    WaitingForOrder,
    Goodbye
}

