using UnityEngine;

public class EndGameWaiter : MonoBehaviour
{
    public int highscore;
    public string reason;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver(int score, string reason)
    {
        highscore = score;
        reason = reason;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game Over");
    }


}
