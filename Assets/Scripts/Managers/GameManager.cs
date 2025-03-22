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

    public int level = 1;
    public bool isGameStarted;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        data.music = SoundManager.Instance.music;
        data.volume = SoundManager.Instance.volume;

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
            SoundManager.Instance.music = data.music;
            SoundManager.Instance.volume = data.volume;
        }
    }
}
