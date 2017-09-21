using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    AudioSource audio;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    public void Play()
    {
        audio.Play();
    }
    public void Play(AudioClip soundClip)
    {
        audio.clip = soundClip;
        audio.Play();
        StartCoroutine(WaitAndEnqueue(soundClip.length));
    }
    IEnumerator WaitAndEnqueue(float time)
    {
        yield return new WaitForSeconds(time);
        SoundManager.CompleteSound(this);
    }
}