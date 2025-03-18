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
    public Collectibles[] collectibles;

    GameManager gameManager;

    int Score = 0;
    void Start()
    {
        gameManager = GameManager.Instance;
        for (int i = 0; i < collectibles.Length; i++)
        {
            collectibles[i].name = collectibles[i].collectible.name;
        }
        DisplayScore();
    }

    public void AddScore(string collectibleName)
    {
        for (int i = 0; i < collectibles.Length; i++)
        {
            if (collectibleName == collectibles[i].name)
            {
                Score += collectibles[i].collectibleValue;
                DisplayScore();
            }
        }
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
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        HealthManager.ResetHealth();
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
