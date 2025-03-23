using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int level = 1;

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

    #region Json Data
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
    #endregion
}
