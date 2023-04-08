using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject[] lockIcons;

    private void Awake()
    {
        
    }
    public void PressShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void PressGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void PressTutorial()
    {
        SceneManager.LoadScene("Intro");
    }

    public void PressMonday()
    {

    }

    public void PressTuesday()
    {

    }

    public void PressWednesday()
    {

    }

    public void PressThursday()
    {

    }

    public void PressFriday()
    {

    }

    public void PressSaturday()
    {

    }

    public void PressSunday()
    {

    }
}
