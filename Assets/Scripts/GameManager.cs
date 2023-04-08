using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }
    private float currentGameSpeed;

    public float clockSpeed;
    private float timer = 0f;
    public int clockHour = 0;
    public int clockMinute = 0;

    public TMP_Text timeText;
    public Image[] livesImages;
    public TMP_Text scoreText;
    public GameObject gameOverScreen;
    public GameObject timesUpScreen;

    public TMP_Text gameOverScores;
    public TMP_Text timesUpScores;

    private Player player;
    private Spawner spawner;

    private int score;
    public bool isPaused;

    private void Awake()
    {
        //PlayerPrefs.SetFloat("hiscore", 0);
        //PlayerPrefs.SetInt("score", 5000);

        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
        }

        score = 0;
    }

    private void OnDestroy()
    {
        if (Instance == this) {
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
        gameOverScreen.SetActive(false);
        timesUpScreen.SetActive(false);
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles) {
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
    }

    public void ReduceLives(int lives)
    {
        livesImages[lives].gameObject.SetActive(false);
    }    

    public void GamePause()
    {
        currentGameSpeed = gameSpeed;
        gameSpeed = 0f;
        isPaused = true;
    }    

    public void GameResume()
    {
        gameSpeed = currentGameSpeed;
        isPaused = false;
    }    

    public void GameOver()
    {
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
        ClearBuffs();
        timesUpScores.SetText("Total scores: " + PlayerPrefs.GetInt("score", 0) + "\n" + "High score: " + PlayerPrefs.GetFloat("hiscore", 0));
    }

    private void UpdateTotalScore()
    {
        int totalScore = PlayerPrefs.GetInt("score", 0);
        totalScore += score;
        PlayerPrefs.SetInt("score", totalScore);
    }

    public void AddTime()
    {
        timer += 15;  
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
        if (!isPaused)
        {
            timer += Time.deltaTime * clockSpeed;
            gameSpeed += gameSpeedIncrease * Time.deltaTime;
        }    
        clockMinute = Mathf.FloorToInt(timer);
        if (timer > 60f)
        {
            ++clockHour;
            timer = 0f;
        }

        if (clockMinute < 10)
        {
            timeText.SetText("Time: 0" + clockHour.ToString() + " : 0" + clockMinute.ToString());
        }
        else timeText.SetText("Time: 0" + clockHour.ToString() + " : " + clockMinute.ToString());

        scoreText.SetText("Score: " + score.ToString());

        if (clockHour == 7)
        {
            TimesUp();
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
