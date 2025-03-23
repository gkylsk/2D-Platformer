using System;
using UnityEngine;

public class Collectible : MonoBehaviour, IItem
{
    public static event Action<int> OnCollected;
    public int value;

    public void Collect()
    {
        OnCollected.Invoke(value);
        SoundManager.Play("Collect");
        Destroy(gameObject);
    }
}
