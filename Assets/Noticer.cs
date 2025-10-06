using System;
using TMPro;
using UnityEngine;

public class Noticer : MonoBehaviour
{
    public EndGameWaiter endGameWaiter;
    TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        endGameWaiter = FindObjectOfType<EndGameWaiter>();
        text = GetComponent<TextMeshProUGUI>();

        text.text = $"Oh no! You served {endGameWaiter.highscore} happy customers!{Environment.NewLine}{endGameWaiter.reason}";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
