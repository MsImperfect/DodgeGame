using System;
using UnityEngine;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float gameSpeed = 20f;
    public float sidewaysForce = 18f;
    public float gameSpeedIncrease = 0.2f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

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
        NewGame();
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
        LoadHighScore();
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        sidewaysForce = 0f;
        player.gameOver = true;
        player.rb.isKinematic = true;
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
        highScoreText.text = "HighScore: " + highScore;
        Debug.Log("High Score Saved: " + highScore);
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
        highScoreText.text = "HighScore: " + highScore;
    }

    [System.Serializable]
    public class HighScoreData
    {
        public int highScore;
    }
}
