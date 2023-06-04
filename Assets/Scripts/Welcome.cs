using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Welcome : MonoBehaviour
{
    public void PressStart()
    {
        SceneManager.LoadScene("MainMenu");
    }    
    public void Credit()
    {
        SceneManager.LoadScene("Credit");
    }    

    public void Guide()
    {
        SceneManager.LoadScene("Guide");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToWelcome()
    {
        SceneManager.LoadScene("Welcome");
    }    
}
