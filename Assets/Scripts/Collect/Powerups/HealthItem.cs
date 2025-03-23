using System;
using UnityEngine;

public class HealthItem : MonoBehaviour, IItem
{
    public static event Action<int> OnHealthCollect;
    int healthAmount = 1;

    public void Collect()
    {
        OnHealthCollect.Invoke(healthAmount);
        SoundManager.Play("Collect");
        Destroy(gameObject);
    }
}
