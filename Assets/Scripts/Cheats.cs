using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{
    public GameObject cheatMenu;
    // Start is called before the first frame update
    private void Awake()
    {
        cheatMenu.SetActive(false);
    }

    public void PressCheat()
    {
        cheatMenu.SetActive(true);
    }

    public void PressHome()
    {
        cheatMenu.SetActive(false);
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("score", 0);
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetFloat("hiscore", 0);
    }

    public void AddScores()
    {
        int score = PlayerPrefs.GetInt("score", 0);
        PlayerPrefs.SetInt("score", score + 1000);
    }

    public void UnlockAllStages()
    {
        PlayerPrefs.SetInt("daysUnlocked", 7);
        SceneManager.LoadScene("MainMenu");
    }

    public void LockAllStages()
    {
        PlayerPrefs.SetInt("daysUnlocked", 0);
        SceneManager.LoadScene("MainMenu");
    }
}
