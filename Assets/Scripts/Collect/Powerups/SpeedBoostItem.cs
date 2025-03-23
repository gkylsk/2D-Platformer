using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostItem : MonoBehaviour, IItem
{
    public static event Action<float> OnSpeedCollect;
    public float speedMultiplyer = 2f;

    public void Collect()
    {
        OnSpeedCollect.Invoke(speedMultiplyer);
        SoundManager.Play("Collect");
        Destroy(gameObject);
    }
}
