using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayUIButtons : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Awake()
    {
        pauseMenu.SetActive(false);
    }

    public void PressPause()
    {
        pauseMenu.SetActive(true);
        GameManager.Instance.GamePause();
    }    

    public void PressResume()
    {
        pauseMenu.SetActive(false);
        GameManager.Instance.GameResume();
    }    

    public void PressReload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }    

    public void PressHome()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
