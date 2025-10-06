using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public CustomerType type;
    public float groundOffset;
    public float driveSpeed;
    public AudioSource audioSource;

    public AudioClip[] driveClips; // Assign 2 clips in the Inspector
    public AudioClip happySoundClip;

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

                if(randomCook < 0.3f)
                {
                    randomCook = 0f; // Raw
                }
                else if (randomCook < 0.5f)
                {
                    randomCook = 0.33f; // Rare
                }
                else if (randomCook < 0.7f)
                {
                    randomCook = 0.66f; // Medium
                }
                else
                {
                    randomCook = 0.9f; // Well Done
                }

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

    public void PlayHappySound()
    {
        if (driveClips != null && driveClips.Length > 0 && audioSource != null)
        {
            int idx = Random.Range(0, driveClips.Length);
            audioSource.clip = happySoundClip;
            audioSource.Play();
        }
    }

    internal void CompleteOrder(bool orderMatches)
    {
        if(state != CustomerStates.WaitingForOrder)
        {
            return;
        }

        if (orderMatches)
        {
            Debug.Log("Order Complete! Customer is happy!");
            GameManager.Instance.happyCustomers += 1;
            state = CustomerStates.Goodbye;
            // Clear order sign
            foreach (Transform child in GameManager.Instance.orderSignHolder.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            // Drive away
            PlayHappySound();
        }
        else
        {

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

