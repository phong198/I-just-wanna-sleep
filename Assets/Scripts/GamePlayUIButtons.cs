using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

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
        DOTween.Clear(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }    

    public void PressHome()
    {
        DOTween.Clear(true);
        SceneManager.LoadScene("MainMenu");
    }
}
