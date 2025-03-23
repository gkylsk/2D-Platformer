using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static int health = 3;

    [SerializeField]
    Image[] hearts;
    [SerializeField] Sprite fullHealth;
    [SerializeField] Sprite halfHealth;
    [SerializeField] Sprite emptyHealth;

    void Update()
    {
        SetHealthSprite();
    }

    void SetHealthSprite()
    {
        //set all heart as empty sprite
        foreach (Image img in hearts)
        {
            img.sprite = emptyHealth;
        }
        //set the full heaart sprite based on health
        for (int i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHealth;
        }
    }
    public static void ResetHealth()
    {
        health = 3;
    }
}
