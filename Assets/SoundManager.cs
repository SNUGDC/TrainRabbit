﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum MusicType { mainTheme, goodMain, badMain, goodRabbit, stangeRabbit, seriousRabbit, stageClear, gameOver, happyEnding, sadEnding, sooneung}
public enum SoundType { click, swing, hit, death, talk, trainDoor, getItem }

[System.Serializable]
public class MusicDic
{
    public MusicType musicType;
    public AudioClip audioClip;
}
[System.Serializable]
public class SoundDic
{
    public SoundType soundType;
    public AudioClip audioClip;
}


public class SoundManager : MonoBehaviour {

    public static GameObject instanceGO = null;
    static SoundManager instance;
    static Queue<SoundPlayer> soundPlayers;
    static List<SoundPlayer> usingPlayers;
    static MusicPlayer mainMusicPlayer;
    static MusicPlayer subMusicPlayer;
    static SoundPlayer talkPlayer;
    static bool isOnTrain = true;
    public GameObject standardSoundPlayer;
    public GameObject standardMusicPlayer;
    public MusicDic[] musicClips;
    public SoundDic[] soundClips;
    public float fadeDuration = 0.5f;

    void Awake()
    {
        FindTrain();
        if (instanceGO == null)
        {
            instance = this;
            instanceGO = this.gameObject;
            soundPlayers = new Queue<SoundPlayer>();
            usingPlayers = new List<SoundPlayer>();
            SoundManager.talkPlayer = Instantiate(standardSoundPlayer, transform).GetComponent<SoundPlayer>();
            SoundManager.mainMusicPlayer = Instantiate(standardMusicPlayer, transform).GetComponent<MusicPlayer>();
            SoundManager.subMusicPlayer = Instantiate(standardMusicPlayer, transform).GetComponent<MusicPlayer>();
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instanceGO != this)
        {
            Destroy(gameObject);
        }
	}
    public static void FindTrain()
    {
        isOnTrain = FindObjectOfType<TrainGenerator>();
        Debug.Log("isOnTrain : " + isOnTrain);
    }
    public static void SetIsOnTrain(bool value)
    {
        isOnTrain = value;
        Debug.Log("isOnTrain : " + isOnTrain);
    }     
    void Start()
    {
        mainMusicPlayer.SetMusic(musicClips.First(a => a.musicType == MusicType.goodMain).audioClip);
        mainMusicPlayer.Play();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isOnTrain)
        {
            SoundManager.PlayClick();
        }
    }
    static void SwitchMusicPlayers()
    {
        var temp = mainMusicPlayer;
        mainMusicPlayer = subMusicPlayer;
        subMusicPlayer = temp;
    }

    public static void PlayOtherMusic(Rabbit rabbit)
    {
        Debug.Log("playOhterMusic");
        mainMusicPlayer.FadeOut(instance.fadeDuration);
        var clip = ChooseMusicClip(rabbit);
        subMusicPlayer.SetMusic(clip);
        subMusicPlayer.FadeIn(instance.fadeDuration);
        SwitchMusicPlayers();
    }
    public static void PlayOtherMusic(MusicType mt)
    {
        Debug.Log("playOhterMusic");
        mainMusicPlayer.FadeOut(instance.fadeDuration);
        var clip = instance.musicClips.First(a => a.musicType == mt).audioClip;
        subMusicPlayer.SetMusic(clip);
        subMusicPlayer.FadeIn(instance.fadeDuration);
        SwitchMusicPlayers();
    }
    static AudioClip ChooseMusicClip(Rabbit rabbit)
    {
        MusicType musicType = MusicType.goodRabbit;
        switch(rabbit)
        {
            case Rabbit.good:
                musicType = MusicType.goodRabbit;
                break;
            case Rabbit.strange:
                musicType = MusicType.stangeRabbit;
                break;
            case Rabbit.serious:
                musicType = MusicType.seriousRabbit;
                break;
        }
        return instance.musicClips.First(a => a.musicType == musicType).audioClip;
    }

    public static void ResumeMainMusic()
    {
        mainMusicPlayer.FadeOut(instance.fadeDuration);
        subMusicPlayer.FadeIn(instance.fadeDuration);
        SwitchMusicPlayers();
    }

    static void PlaySound(SoundType st)
    {
        var clip = ChooseSoundClip(st);
        if (soundPlayers.Count > 0)
        {
            var sp = soundPlayers.Dequeue();
            usingPlayers.Add(sp);
            sp.Play(clip);
        }
        else
        {
            var spGO = Instantiate(instance.standardSoundPlayer, instanceGO.transform);
            var sp = spGO.GetComponent<SoundPlayer>();
            usingPlayers.Add(sp);
            sp.Play(clip);
        }
    }
    public static void CompleteSound(SoundPlayer sp)
    {
        usingPlayers.Remove(sp);
        soundPlayers.Enqueue(sp);
    }
    static AudioClip ChooseSoundClip(SoundType st)
    {
        AudioClip result;
        var iclips = 
            from sc in instance.soundClips
            where sc.soundType == st
            select sc.audioClip;
        var clips = iclips.ToArray();
        if (clips.Length > 0)
        {
            result = clips[(int)(Random.value*clips.Length)];
        }
        else
        {
            result = null;
        }
        return result;
    }

    public static void PlayClick()
    {
        PlaySound(SoundType.click);
    }
    public static void PlayDeath()
    {
        PlaySound(SoundType.death);
    }
    public static void PlayHit()
    {
        PlaySound(SoundType.hit);
    }
    public static void PlaySwing()
    {
        PlaySound(SoundType.swing);
    }
    public static void PlayTalk()
    {
        var clip = ChooseSoundClip(SoundType.talk);
        talkPlayer.PlaySolo(clip);
    }
    public static void PlayTrainDoor()
    {
        PlaySound(SoundType.trainDoor);
    }
    public static void PlayGetItem()
    {
        PlaySound(SoundType.getItem);
    }
}
