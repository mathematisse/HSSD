using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static soundManager instance;
    public float musicVolume = 0.15f;
    public float effectVolume = 1;
    public AudioClip[] clips;

    public AudioSource musicSource;
    public AudioSource effectSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        musicSource.volume = 0.1f;
    }

    private void Update()
    {
        musicSource.volume = musicVolume;
        effectSource.volume = effectVolume;
    }

    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    public void StopSound()
    {
        effectSource.Stop();
    }

    public void PlayManagerClip(int index)
    {
        effectSource.PlayOneShot(clips[index]);
    }

    public void LoopManagerClip(int index)
    {
        effectSource.clip = clips[index];
        effectSource.loop = true;
        effectSource.Play();
    }
}
