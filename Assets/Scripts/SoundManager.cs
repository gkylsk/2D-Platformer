using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private static AudioSource audioSource;
    private static SoundEffectLibrary soundEffectLibrary;

    public float volume;
    public bool music;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        soundEffectLibrary = GetComponent<SoundEffectLibrary>();
    }
    void Start()
    {
        
    }

    public static void Play(string name)
    {
        AudioClip clip = soundEffectLibrary.GetAudioClip(name);
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, 5f);
        }
    }
    public void SetVolume()
    {
        audioSource.volume = volume;
    }

    public void PlayMusic()
    {
        audioSource.Play();
        music = true;
    }
    public void StopMusic()
    {
        audioSource.Stop();
        music = false;
    }
}
