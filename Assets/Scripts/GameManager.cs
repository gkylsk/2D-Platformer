using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public struct Collectibles
    {
        public string name;
        public GameObject collectible;
        public int collectibleValue;
    }

    public static GameManager Instance;
    public Collectibles[] collectibles;

    [SerializeField] Text scoreText;

    //score
    int Score = 0;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        for (int i = 0; i < collectibles.Length; i++)
        {
            collectibles[i].name = collectibles[i].collectible.name;
        }
        DisplayScore();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
