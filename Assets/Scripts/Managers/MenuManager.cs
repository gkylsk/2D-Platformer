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
        gameManager.LoadLevel();
        InitializeLevelButtons();
        levelText.text = gameManager.level.ToString();
    }

    public void StartGame()
    {
        OpenLevel(gameManager.level);
    }

    void InitializeLevelButtons()
    {
        int unloacked = gameManager.level;
        //set all buttons as non interactable
        for(int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }
        //set the level button interactable if level is unlocked
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
