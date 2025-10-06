using UnityEngine;

public class EndGameWaiter : MonoBehaviour
{
    public int highscore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver(int score)
    {
        highscore = score;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game Over");
    }


}
