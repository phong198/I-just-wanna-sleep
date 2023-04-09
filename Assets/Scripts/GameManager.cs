using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease;
    public float gameSpeed { get; private set; }
    private float currentGameSpeed;

    public int clockSpeed;
    public int clockHour = 0;
    public int clockMinute = 0;

    public GameObject[] moms;
    private int momIndex = 0;
    public TMP_Text dayText;
    public TMP_Text timeText;
    public Image[] livesImages;
    public GameObject timeBuff;
    public TMP_Text scoreText;
    public GameObject gameOverScreen;
    public GameObject timesUpScreen;
    public TMP_Text winLoseText;

    public TMP_Text gameOverScores;
    public TMP_Text timesUpScores;

    public AudioSource dailyMusic;
    public AudioSource weekendMusic;
    public MeshRenderer backGround;
    public Material mat1;
    public Material mat2;

    private Player player;
    private Spawner spawner;

    private int score;
    public bool isPaused;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }

        score = 0;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        NewGame();
    }

    public void NewGame()
    {
        Time.timeScale = 1;
        gameOverScreen.SetActive(false);
        timesUpScreen.SetActive(false);
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        gameSpeed = initialGameSpeed;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);

        UpdateHiscore();

        livesImages[3].gameObject.SetActive(false);
        if (PlayerPrefs.GetInt("isBoughtLive", 0) == 1)
        {
            livesImages[3].gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt("isBoughtTime", 0) == 1)
        {
            timeBuff.SetActive(true);
            if (PlayerPrefs.GetString("day", "Monday") == "Saturday" || PlayerPrefs.GetString("day", "Monday") == "Sunday")
            {
                MinusTime();
                MinusTime();
            }
            else AddTime();
        }

        if (PlayerPrefs.GetString("day", "Monday") == "Saturday" || PlayerPrefs.GetString("day", "Monday") == "Sunday")
        {
            weekendMusic.Play(0);
            backGround.material = mat2;
        }
        else
        {
            dailyMusic.Play(0);
            backGround.material = mat1;
        }

        dayText.SetText(PlayerPrefs.GetString("day", "Monday"));
        dayText.DOFade(0, 3).SetDelay(3);

        StartCoroutine(RunClock());
    }

    IEnumerator RunClock()
    {
        while (clockMinute <= 60)
        {
            clockMinute += clockSpeed;
            if (clockMinute >= 60)
            {
                clockMinute = 0;
                clockHour += 1;
            }

            if (PlayerPrefs.GetString("day", "Monday") != "Saturday" && PlayerPrefs.GetString("day", "Monday") != "Sunday")
            {
                if (clockHour == 5 && clockMinute == 30 || clockHour == 6 && clockMinute == 00 || clockHour == 6 && clockMinute == 30)
                {
                    moms[momIndex].SetActive(true);
                    moms[momIndex].transform.DOMoveX(-12, 5);
                    ++momIndex;
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
    public void ReduceLives(int lives)
    {
        livesImages[lives].gameObject.SetActive(false);
    }

    public void GamePause()
    {
        Time.timeScale = 0;
        currentGameSpeed = gameSpeed;
        gameSpeed = 0f;
        isPaused = true;
    }

    public void GameResume()
    {
        Time.timeScale = 1;
        gameSpeed = currentGameSpeed;
        isPaused = false;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        UpdateHiscore();
        UpdateTotalScore();
        gameOverScreen.SetActive(true);
        ClearBuffs();
        gameOverScores.SetText("Total scores: " + PlayerPrefs.GetInt("score", 0) + "\n" + "High score: " + PlayerPrefs.GetFloat("hiscore", 0));

    }

    public void TimesUp()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        UpdateHiscore();
        UpdateTotalScore();
        timeText.SetText("07 : 00");
        timesUpScreen.SetActive(true);
        winLoseText.SetText("Time's Up");
        ClearBuffs();
        timesUpScores.SetText("Total scores: " + PlayerPrefs.GetInt("score", 0) + "\n" + "High score: " + PlayerPrefs.GetFloat("hiscore", 0));
    }

    public void YouWin()
    {
        Time.timeScale = 0;
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        UpdateHiscore();
        UpdateTotalScore();
        timeText.SetText("07 : 00");
        timesUpScreen.SetActive(true);
        winLoseText.SetText("You win");
        ClearBuffs();
        timesUpScores.SetText("Total scores: " + PlayerPrefs.GetInt("score", 0) + "\n" + "High score: " + PlayerPrefs.GetFloat("hiscore", 0));

        int daysUnlocked = PlayerPrefs.GetInt("daysUnlocked", 0);
        if (daysUnlocked < 7)
        {
            PlayerPrefs.SetInt("daysUnlocked", daysUnlocked + 1);
        }
    }

    private void UpdateTotalScore()
    {
        int totalScore = PlayerPrefs.GetInt("score", 0);
        totalScore += score;
        PlayerPrefs.SetInt("score", totalScore);
    }

    public void AddTime()
    {
        clockMinute += 15;
        if (clockMinute >= 60)
        {
            int extraTime = clockMinute - 60;
            clockMinute = extraTime;
            clockHour += 1;
        }
    }

    public void MinusTime()
    {
        clockMinute -= 15;
        if (clockMinute < 0)
        {
            int extraTime = 60 - 15 - clockMinute;
            clockMinute = extraTime;
            clockHour -= 1;
            if (clockHour < 0)
            {
                if ((PlayerPrefs.GetString("day", "Monday") != "Saturday" && PlayerPrefs.GetString("day", "Monday") != "Sunday"))
                {
                    clockHour = 0;
                    clockMinute = 0;
                }    
                //clockHour = 0;
                //clockMinute = 0;
            }
        }
    }

    public void AddScore()
    {
        score++;
    }

    private void ClearBuffs()
    {
        PlayerPrefs.SetInt("isBoughtLive", 0);
        PlayerPrefs.SetInt("isBoughtTime", 0);
    }


    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;

        if (clockMinute < 10)
        {
            timeText.SetText("Time: 0" + clockHour.ToString() + " : 0" + clockMinute.ToString());
        }
        else timeText.SetText("Time: 0" + clockHour.ToString() + " : " + clockMinute.ToString());

        if (clockHour < 0)
        {
            timeText.SetText("Time: 00 : 00");
        }

        scoreText.SetText("Score: " + score.ToString());

        if (clockHour == 7)
        {
            if (PlayerPrefs.GetString("day", "Monday") == "Saturday" || PlayerPrefs.GetString("day", "Monday") == "Sunday")
            {
                YouWin();
            }
            else TimesUp();
        }
    }

    private void UpdateHiscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }
    }
}
