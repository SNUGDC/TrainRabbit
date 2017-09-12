using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audio;
    float absVolume;
    float initTime;
    float finalTime;
    float initVolume;
    float finalVolume;
    bool isFading;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        absVolume = audio.volume;
        Initiate();
    }
    void Initiate()
    {
        audio.volume = absVolume;
        isFading = false;
    }
    void FixedUpdate()
    {
        if (isFading)
        {
            audio.volume = initVolume + 
                (finalVolume - initVolume)
                /(finalTime - initTime)
                *(Time.time - initTime); 
            if (Time.time > finalTime)
            {
                isFading = false;
            }
        }
    }
    public void SetMusic(AudioClip musicClip)
    {
        audio.clip = musicClip;
    }
    public void Play()
    {
        audio.Play();
    }
    public void Play(AudioClip musicClip)
    {
        audio.clip = musicClip;
        audio.Play();
    }
    public void FadeOut(float duration)
    {
        initVolume = audio.volume;
        finalVolume = 0;
        initTime = Time.time;
        finalTime = initTime + duration;
        isFading = true;
        StartCoroutine(Pause(duration));
    }
    public void FadeIn(float duration)
    {
        initVolume = 0;
        finalVolume = absVolume;
        initTime = Time.time;
        finalTime = initTime + duration;
        isFading = true;
        audio.Play();
    }
    IEnumerator Pause(float duration)
    {
        yield return new WaitForSeconds(duration);
        audio.Pause();
    }
}