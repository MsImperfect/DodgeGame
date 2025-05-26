using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float gameSpeed = 20f;
    public float sidewaysForce = 18f;
    public float gameSpeedIncrease = 0.2f;

    private float score;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.gameOver)
        {
            gameSpeed += gameSpeedIncrease * Time.deltaTime;
            sidewaysForce += gameSpeedIncrease * 1.11f * Time.deltaTime;
            score += gameSpeed * Time.deltaTime;
        }
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        sidewaysForce = 0f;
        player.gameOver = true;
    }
}
