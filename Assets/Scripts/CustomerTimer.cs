using UnityEngine;
using TMPro;

public class CustomerTimer : MonoBehaviour
{
    [Header("Timer Settings (Set by GameManager)")]
    public float totalTimeSeconds; // e.g., 3 minutes per customer
    private float currentTime;
    private bool isRunning = false;

    [Header("UI (Optional)")]
    public TextMeshPro timerText; // Assign in Inspector if you want it visible

    void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;
        if (currentTime < 0)
        {
            currentTime = 0;
            isRunning = false;
            OnTimerEnd();
        }

        UpdateDisplay();
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        currentTime = totalTimeSeconds;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (!timerText) return;

        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void OnTimerEnd()
    {
        Debug.Log($"Customer {name}'s timer ended!");
        GameManager.Instance.currentCustomer.OnTimeExpired();
    }

    // Optionally expose time fraction for progress bars, etc.
    public float GetTimeNormalized()
    {
        return currentTime / totalTimeSeconds;
    }
}
