using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Image img in  hearts)
        {
            img.sprite = emptyHealth;
        }
        for(int i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHealth;
        }
    }
}
