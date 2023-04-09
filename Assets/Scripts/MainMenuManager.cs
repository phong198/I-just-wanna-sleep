using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject[] lockIcons;

    private void Awake()
    {
        foreach (GameObject lockIcon in lockIcons)
        {
            lockIcon.SetActive(true);
        }
        int daysUnlocked = PlayerPrefs.GetInt("daysUnlocked", 0);
        for (int i=0; i < daysUnlocked; i++)
        {
            lockIcons[i].SetActive(false);
        }
    }
    public void PressShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void PressTutorial()
    {
        SceneManager.LoadScene("Intro");
    }

    public void PressMonday()
    {
        if (PlayerPrefs.GetInt("daysUnlocked", 0) > 0)
        {
            PlayerPrefs.SetString("day", "Monday");
            SceneManager.LoadScene("Gameplay");
        }
    }

    public void PressTuesday()
    {
        if (PlayerPrefs.GetInt("daysUnlocked", 0) > 1)
        {
            PlayerPrefs.SetString("day", "Tuesday");
            SceneManager.LoadScene("Gameplay");
        }
    }

    public void PressWednesday()
    {
        if (PlayerPrefs.GetInt("daysUnlocked", 0) > 2)
        {
            PlayerPrefs.SetString("day", "Wednesday");
            SceneManager.LoadScene("Gameplay");
        }
    }

    public void PressThursday()
    {
        if (PlayerPrefs.GetInt("daysUnlocked", 0) > 3)
        {
            PlayerPrefs.SetString("day", "Thursday");
            SceneManager.LoadScene("Gameplay");
        }
    }

    public void PressFriday()
    {
        if (PlayerPrefs.GetInt("daysUnlocked", 0) > 4)
        {
            PlayerPrefs.SetString("day", "Friday");
            SceneManager.LoadScene("Gameplay");
        }
    }

    public void PressSaturday()
    {
        if (PlayerPrefs.GetInt("daysUnlocked", 0) > 5)
        {
            PlayerPrefs.SetString("day", "Saturday");
            SceneManager.LoadScene("Gameplay");
        }
    }

    public void PressSunday()
    {
        if (PlayerPrefs.GetInt("daysUnlocked", 0) > 6)
        {
            PlayerPrefs.SetString("day", "Sunday");
            SceneManager.LoadScene("Gameplay");
        }
    }
}
