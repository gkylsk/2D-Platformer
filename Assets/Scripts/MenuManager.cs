using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] GameObject mainMenuScreen;
    [SerializeField] Text levelText;
    
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        mainMenuScreen.SetActive(false);
        LoadLevel(gameManager.level);
    }

    void LoadLevel(int level)
    {
        string levelName = "Level" + level;
        SceneManager.LoadScene(levelName);
        gameManager.SaveLevel();
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
