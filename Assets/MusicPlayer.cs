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
    bool isPlaying;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        absVolume = audio.volume;
        Initiate();
    }
    void Initiate()
    {
        audio.volume = absVolume;
        audio.loop = true;
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
    public bool GetIsPlaying()
    {
        return isPlaying;
    }
    public void Play()
    {
        Play(true);
    }
    public void Play(bool loop)
    {
        audio.volume = absVolume;
        audio.loop = loop;
        isPlaying = true;
        audio.Play();
        if (!loop)
        {
            StopAllCoroutines();
            StartCoroutine(Pause(audio.clip.length, false));
        }
    }
    public void FadeOut(float duration, bool doPause)
    {
        initVolume = audio.volume;
        finalVolume = 0;
        initTime = Time.time;
        finalTime = initTime + duration;
        isFading = true;
        isPlaying = false;

        StopAllCoroutines();
        StartCoroutine(Pause(duration, doPause));
    }
    public void FadeIn(float duration)
    {
        initVolume = 0;
        finalVolume = absVolume;
        initTime = Time.time;
        finalTime = initTime + duration;
        isFading = true;
        isPlaying = true;
        audio.loop = true;
        audio.Play();
    }
    IEnumerator Pause(float duration, bool doPause)
    {
        yield return new WaitForSeconds(duration);
        isPlaying = false;
        if (doPause)
        {
            audio.Pause();
        }
        else
        {
            audio.Stop();
        }
    }
}