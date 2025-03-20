using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Button[] levelButtons;
    [SerializeField] Text levelText;
    
    GameManager gameManager;

    private void Awake()
    {
        
    }
    void Start()
    {
        gameManager = GameManager.Instance;
        InitializeLevelButtons();
        levelText.text = gameManager.level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        OpenLevel(gameManager.level);
    }

    void InitializeLevelButtons()
    {
        int unloacked = gameManager.level;
        for(int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }
        for(int i = 0; i < unloacked; i++)
        {
            levelButtons[i].interactable = true;
        }
    }
    public void OpenLevel(int level)
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
