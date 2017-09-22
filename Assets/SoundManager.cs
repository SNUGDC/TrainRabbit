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


public class SoundManager : MonoBehaviour {

    public static GameObject instanceGO = null;
    static SoundManager instance;
    static Queue<SoundPlayer> soundPlayers;
    static List<SoundPlayer> usingPlayers;
    static MusicPlayer mainMusicPlayer;
    static MusicPlayer subMusicPlayer;
    public GameObject standardSoundPlayer;
    public GameObject standardMusicPlayer;
    public MusicDic[] musicClips;
    public SoundDic[] soundClips;
    public float fadeDuration = 0.5f;

    void Awake()
    {
        if (instanceGO == null)
        {
            instance = this;
            instanceGO = this.gameObject;
            soundPlayers = new Queue<SoundPlayer>();
            usingPlayers = new List<SoundPlayer>();
            SoundManager.mainMusicPlayer = Instantiate(standardMusicPlayer, transform).GetComponent<MusicPlayer>();
            SoundManager.subMusicPlayer = Instantiate(standardMusicPlayer, transform).GetComponent<MusicPlayer>();
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instanceGO != this)
        {
            Destroy(gameObject);
        }
	}

    void Start()
    {
        mainMusicPlayer.SetMusic(musicClips.First(a => a.musicType == MusicType.goodMain).audioClip);
        mainMusicPlayer.Play();
    }

    /*public void ChangeMainMusic(MusicType musicType)
    {
        mainMusic.Stop();
        mainMusic.clip = musicClips.First(a => a.musicType == musicType).audioClip;
        mainMusic.Play();
    }*/

    public static void PlayOtherMusic(Rabbit rabbit)
    {
        Debug.Log("playOhterMusic");
        mainMusicPlayer.FadeOut(instance.fadeDuration);
        var clip = ChooseMusicClip(rabbit);
        subMusicPlayer.SetMusic(clip);
        subMusicPlayer.FadeIn(instance.fadeDuration);
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
        subMusicPlayer.FadeOut(instance.fadeDuration);
        mainMusicPlayer.FadeIn(instance.fadeDuration);
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
        PlaySound(SoundType.talk);
    }
}
