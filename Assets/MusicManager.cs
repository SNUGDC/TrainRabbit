using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum MusicType { goodMain, badMain, goodRabbit, stangeRabbit, seriousRabbit}
public enum SoundType { click, swing, hit, death, talk }

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


public class MusicManager : MonoBehaviour {

    public static GameObject instance = null;
    public static Queue<SoundPlayer> soundPlayers;
    public static List<SoundPlayer> usingPlayers;
    public GameObject standardSoundPlayer;
    public AudioSource mainMusic;
    public AudioSource otherMusic;
    public AudioSource click;
    public AudioSource death;
    public AudioSource hit;

    public MusicDic[] musicClips;
    public SoundDic[] soundClips;

    void Awake()
    {
        if (instance == null)
        {
            instance = this.gameObject;
            soundPlayers = new Queue<SoundPlayer>();
            usingPlayers = new List<SoundPlayer>();
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
	}

    void Start()
    {
        mainMusic.Play();
    }

    public void ChangeMainMusic(MusicType musicType)
    {
        mainMusic.Stop();
        mainMusic.clip = musicClips.First(a => a.musicType == musicType).audioClip;
        mainMusic.Play();
    }


    public void PlayOtherMusic(Rabbit rabbit)
    {
        Debug.Log("playOhterMusic");
        mainMusic.Stop();
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
        otherMusic.clip = musicClips.First(a => a.musicType == musicType).audioClip;
        otherMusic.Play();
    }

    void PlaySound(SoundType st)
    {
        var clip = ChooseClip(st);
        if (soundPlayers.Count > 0)
        {
            var sp = soundPlayers.Dequeue();
            usingPlayers.Add(sp);
            sp.Play(clip);
        }
        else
        {
            var spGO = Instantiate(standardSoundPlayer, instance.transform);
            var sp = spGO.GetComponent<SoundPlayer>();
            usingPlayers.Add(sp);
            sp.Play(clip);
        }
    }
    AudioClip ChooseClip(SoundType st)
    {
        AudioClip result;
        var iclips = 
            from sc in soundClips
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
    public void PlayClick()
    {
        PlaySound(SoundType.click);
    }

    public void PlayDeath()
    {
        PlaySound(SoundType.death);
    }
    public void PlayHit()
    {
        PlaySound(SoundType.hit);
    }


    public void ResumeMainMusic()
    {
        otherMusic.Stop();
        mainMusic.Play();
    }

    //public void PlayOtherMusic(AudioClip )
}
