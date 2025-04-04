using System;
using UnityEngine;

public class HealthItem : MonoBehaviour, IItem
{
    public static event Action OnHealthCollect;

    public void Collect()
    {
        OnHealthCollect.Invoke();
        SoundManager.Play("Collect");
        Destroy(gameObject);
    }
}
