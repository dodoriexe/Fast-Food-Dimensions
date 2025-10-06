using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneboy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*
        if(SceneManager.GetActiveScene().name.Contains("Menu"))
        {
            GetComponent<AudioSource>().Play();
        }
        */

        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToMainMenu()
    {
        try
        {
            Destroy(GameManager.Instance.gameObject);
        }
        catch (System.Exception)
        {

        }
        SceneManager.LoadScene("Main Menu");
    }

    public void ToGameScreen()
    {
        Debug.Log("To Game Screen");
        SceneManager.LoadScene("Room");
    }

    public void ToAboutScreen()
    {
        Debug.Log("To Game Screen");
        SceneManager.LoadScene("About");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
