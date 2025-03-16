using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [Header("Main Menu")]
    [SerializeField] GameObject mainMenuScreen;
    [SerializeField] Text levelText;
    AudioSource audioSource;

    [Header("Loading")]
    [SerializeField] GameObject loadingScreen;

    [Header("InGame")]
    [SerializeField] GameObject inGameScreen;
    public Collectibles[] collectibles;

    [SerializeField] Text scoreText;

    //score
    int Score = 0;
    public int level = 1;
    public bool music;
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
        audioSource = GetComponent<AudioSource>();

        levelText.text = level.ToString();
        LoadLevel();
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

    public void StartGame()
    {
        mainMenuScreen.SetActive(false);
        StartCoroutine(LoadingScren());
        LoadLevel(level);
        inGameScreen.SetActive(true);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void MainMenu()
    {
        inGameScreen.SetActive(false);
        StartCoroutine(LoadingScren());
        mainMenuScreen.SetActive(true);
    }

    IEnumerator LoadingScren()
    {
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(5f);
        loadingScreen.SetActive(false);
    }

    void LoadLevel(int level)
    {
        string levelName = "Level" + level;
        SceneManager.LoadScene(levelName);
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


    [System.Serializable]
    class SaveData
    {
        public int level;
        public bool music;
        public float volume;
    }

    public void SaveLevel()
    {
        SaveData data = new SaveData();
        data.level = level;
        data.music = music;
        data.volume = audioSource.volume;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath + "/saveFile.json", json);
    }

    public void LoadLevel()
    {
        string path = Application.dataPath + "/saveFile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            level = data.level;
            music = data.music;
            audioSource.volume = data.volume;
        }
    }
}
