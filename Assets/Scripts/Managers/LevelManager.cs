using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static GameManager;

public class LevelManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject inGameScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject levelWonScreen;
    [SerializeField] Text scoreText;
    
    [SerializeField] int curentLevel;
    GameManager gameManager;

    int Score = 0;
    void Start()
    {
        Reset();
        gameManager = GameManager.Instance;
        DisplayScore();
    }

    private void OnEnable()
    {
        Collectible.OnCollected += AddScore;
    }
    private void OnDestroy()
    {
        Collectible.OnCollected -= AddScore;
    }
    public void AddScore(int score)
    {
        Score += score;
        DisplayScore();
    }

    void DisplayScore()
    {
        scoreText.text = "Score:" + Score;
    }

    public void DisplayGameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }

    public void LevelWon()
    {
        levelWonScreen.SetActive(true);
        if (curentLevel == gameManager.level)
        {
            gameManager.level++;
        }
        gameManager.SaveLevel();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // same level
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // next level
        HealthManager.ResetHealth();
    }

    private void Reset()
    {
        Time.timeScale = 1;
        HealthManager.ResetHealth();
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
