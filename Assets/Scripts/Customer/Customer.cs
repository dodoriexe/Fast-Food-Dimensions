using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public CustomerType type;
    public float groundOffset;
    public float driveSpeed;
    public AudioSource audioSource;

    public bool unhappy = false;

    public AudioClip[] driveClips; // Assign 2 clips in the Inspector
    public AudioClip happySoundClip;
    public AudioClip sadSoundClip;

    public CustomerStates state;

    public List<OrderItemHolder> order;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<OrderItemHolder> myOrder = (GameManager.Instance.GenerateOrder());

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
                GameManager.Instance.StartTimer();
                break;
            case CustomerStates.WaitingForOrder:
                break;
            case CustomerStates.Goodbye:
                transform.position += Vector3.forward * driveSpeed;
                break;
            default:
                break;
        }
    }
    private System.Collections.IEnumerator ShowOrderWithDelay(float delay)
    {
        foreach (var item in order)
        {
            if (item != null)
            {
                float randomCook = item.GetCookPercentage();

                GameObject orderObj = Instantiate(item.GetOrderItem());

                if(randomCook == 0)
                {
                    randomCook = 0f; // Raw
                }
                else if (randomCook < .33f)
                {
                    randomCook = 0.1f; // Rare
                }
                else if (randomCook < 0.66f)
                {
                    randomCook = 0.33f; // Medium
                }
                else
                {
                    randomCook = 0.66f; // Well Done
                }

                orderObj.GetComponent<OrderItem>().Initialize(randomCook, item.GetFoodType(), item.GetFoodSprite());

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

    public void PlaySadSound()
    {
        if (driveClips != null && driveClips.Length > 0 && audioSource != null)
        {
            int idx = Random.Range(0, driveClips.Length);
            audioSource.clip = sadSoundClip;
            audioSource.Play();
        }
    }

    internal void CompleteOrder(bool orderMatches)
    {
        if(state != CustomerStates.WaitingForOrder)
        {
            return;
        }
        Debug.Log($"Order Complete Called! Order Matches: {orderMatches}");

        if (orderMatches)
        {
            GameManager.Instance.TableTop.ClearBag();
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
            GameManager.Instance.customerTimer.PauseTimer();
        }
        else
        {
            // Didn't match order
            GameManager.Instance.TableTop.ClearBag();
            Debug.Log("Order Complete! But the Customer is unhappy!");
            unhappy = true;

            // Clear order sign
            foreach (Transform child in GameManager.Instance.orderSignHolder.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            PlaySadSound();
            GameManager.Instance.customerTimer.PauseTimer();
            state = CustomerStates.Goodbye;
        }
    }

    internal void OnTimeExpired()
    {
        if (state != CustomerStates.WaitingForOrder)
        {
            return;
        }
        Debug.Log("Customer time expired! They are leaving unhappy.");
        
        // Deal Damage(?)

        GameManager.Instance.TableTop.ClearBag();
        // Clear order sign
        foreach (Transform child in GameManager.Instance.orderSignHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameManager.Instance.GameOver("You weren't fast enough with the order!");
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

