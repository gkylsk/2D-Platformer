using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static SoundManager soundManager;
    [SerializeField] Slider slider;

    [SerializeField] GameObject musicOn;
    [SerializeField] GameObject musicOff;

    void Start()
    {
        soundManager = SoundManager.Instance;
        slider.onValueChanged.AddListener(delegate { SetVolumeSlider(); });
        GameManager.Instance.LoadLevel();
        SetSlider();
    }

    void Update()
    {
        
    }

    public void SetVolumeSlider()
    {
        soundManager.volume = slider.value;
        soundManager.SetVolume();
    }

    public void SetSlider()
    {
        slider.value = soundManager.volume;
    }
    public void PlayMusic()
    {
        soundManager.PlayMusic();
        musicOff.SetActive(false);
        musicOn.SetActive(true);
    }
    public void StopMusic()
    {
        soundManager.StopMusic();
        musicOn.SetActive(false);
        musicOff.SetActive(true);
    }
}
