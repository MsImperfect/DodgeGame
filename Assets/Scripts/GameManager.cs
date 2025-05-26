using System;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float gameSpeed = 20f;
    public float sidewaysForce = 18f;
    public float gameSpeedIncrease = 0.2f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject gameOverParent;
    public Button retryButton;

    private float score;
    private int highScore;
    private string filePath;
    private PlayerMovement player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        player = FindAnyObjectByType<PlayerMovement>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        filePath = Application.persistentDataPath + "/HighScore.json";
        gameOverParent.SetActive(false);
        score = 0;
        retryButton.gameObject.SetActive(false);
        LoadHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.gameOver)
        {
            gameSpeed += gameSpeedIncrease * Time.deltaTime;
            sidewaysForce += gameSpeedIncrease * 1.11f * Time.deltaTime;
            score += gameSpeed * Time.deltaTime;
            scoreText.text = Mathf.FloorToInt(score).ToString();
        }
    }

    public void NewGame()
    {
        Debug.Log("NewGame triggered");
        score = 0;
        Time.timeScale = 1f; // Just in case it was paused
        SceneManager.LoadScene("Game");
        //gameSpeed = 20f;
        //sidewaysForce = 18f;
        //player.gameOver = false;
        //player.rb.isKinematic = false;
        //player.transform.position = new Vector3(0, 1.4f, 5); 
        //player.rb.linearVelocity = Vector3.zero;
        //player.rb.angularVelocity = Vector3.zero;
        //gameOverText.enabled = false;
        //gameOverParent.SetActive(false);
        //retryButton.gameObject.SetActive(false);
        //LoadHighScore();
        //Time.timeScale = 1f;
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        sidewaysForce = 0f;
        player.gameOver = true;
        player.rb.isKinematic = true;
        gameOverText.enabled=true;
        gameOverParent.SetActive(true);
        retryButton.gameObject.SetActive(true);
        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        int finalScore = Mathf.FloorToInt(score);

        if (finalScore > highScore)
        {
            highScore = finalScore;
            SaveHighScore();
        }
    }

    private void SaveHighScore()
    {
        HighScoreData data = new HighScoreData { highScore = highScore };
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        highScoreText.text = "TopScore: " + highScore;
        Debug.Log("Top Score Saved: " + highScore);
    }

    private void LoadHighScore()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);
            highScore = data.highScore;
        }
        else
        {
            highScore = 0;
        }
        highScoreText.text = "TopScore: " + highScore;
    }

    [System.Serializable]
    public class HighScoreData
    {
        public int highScore;
    }
}
