using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject boughtLiveImage;
    public GameObject boughtTimeImage;
    public TMP_Text scoreText;
    private int currentScore;
    // Start is called before the first frame update
    private void Start()
    {
        currentScore = PlayerPrefs.GetInt("score", 5000);
        int isBoughtLive = PlayerPrefs.GetInt("isBoughtLive", 0);
        int isBoughtTime = PlayerPrefs.GetInt("isBoughtTime", 0);
        if (isBoughtLive == 1)
        {
            boughtLiveImage.SetActive(true);
        }
        if (isBoughtTime == 1)
        {
            boughtTimeImage.SetActive(true);
        }
        ShowScore();
    }

    public void BuyLive()
    {
        if (currentScore >= 500 && PlayerPrefs.GetInt("isBoughtLive", 0) == 0)
        {
            PlayerPrefs.SetInt("isBoughtLive", 1);
            boughtLiveImage.SetActive(true);
            currentScore -= 500;
            PlayerPrefs.SetInt("score", currentScore);
            ShowScore();
        }
    }

    public void BuyTime()
    {
        if (currentScore >= 200 && PlayerPrefs.GetInt("isBoughtTime", 0) == 0)
        {
            PlayerPrefs.SetInt("isBoughtTime", 1);
            boughtTimeImage.SetActive(true);
            currentScore -= 200;
            PlayerPrefs.SetInt("score", currentScore);
            ShowScore();
        }
    }

    public void PressBack()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void ShowScore()
    {
        scoreText.SetText("Score: " + currentScore.ToString());
    }
}
