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
    static MusicManager theMM;
    static Queue<SoundPlayer> soundPlayers;
    static List<SoundPlayer> usingPlayers;
    MusicPlayer mainMusicPlayer;
    MusicPlayer subMusicPlayer;
    public GameObject standardSoundPlayer;
    public GameObject standardMusicPlayer;
    public MusicDic[] musicClips;
    public SoundDic[] soundClips;
    public float fadeDuration = 0.5f;

    void Awake()
    {
        if (instance == null)
        {
            theMM = this;
            instance = this.gameObject;
            soundPlayers = new Queue<SoundPlayer>();
            usingPlayers = new List<SoundPlayer>();
            mainMusicPlayer = Instantiate(standardMusicPlayer, transform).GetComponent<MusicPlayer>();
            subMusicPlayer = Instantiate(standardMusicPlayer, transform).GetComponent<MusicPlayer>();
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
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

    public void PlayOtherMusic(Rabbit rabbit)
    {
        Debug.Log("playOhterMusic");
        mainMusicPlayer.FadeOut(fadeDuration);
        var clip = ChooseMusicClip(rabbit);
        subMusicPlayer.SetMusic(clip);
        subMusicPlayer.FadeIn(fadeDuration);
    }
    AudioClip ChooseMusicClip(Rabbit rabbit)
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
        return musicClips.First(a => a.musicType == musicType).audioClip;
    }
    public void ResumeMainMusic()
    {
        subMusicPlayer.FadeOut(fadeDuration);
        mainMusicPlayer.FadeIn(fadeDuration);
    }

    void PlaySound(SoundType st)
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
            var spGO = Instantiate(standardSoundPlayer, instance.transform);
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
    AudioClip ChooseSoundClip(SoundType st)
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
        theMM.PlaySound(SoundType.click);
    }
    public void PlayDeath()
    {
        theMM.PlaySound(SoundType.death);
    }
    public void PlayHit()
    {
        theMM.PlaySound(SoundType.hit);
    }
}
