using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum Music { goodMain, badMain, goodRabbit, stangeRabbit, sympatheic}

[System.Serializable]
public class MusicDic
{
    public Music music;
    public AudioClip audioClip;
}

public class MusicManager : MonoBehaviour {

    public static MusicManager instance = null;

    public AudioSource mainMusic;
    public AudioSource otherMusic;

    public MusicDic[] audioClip;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Debug.Log("destroy musicmanager");
            Destroy(gameObject);
        }
	}

    void Start()
    {
        mainMusic.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeMainMusic(Music music)
    {
        mainMusic.Stop();
        mainMusic.clip = audioClip.First(a => a.music == music).audioClip;
        mainMusic.Play();
    }

    public void PlayOtherMusic(Rabbit rabbit)
    {
        Debug.Log("playOhterMusic");
        mainMusic.Stop();
        Music music = Music.goodRabbit;
        switch(rabbit)
        {
            case Rabbit.good:
                music = Music.goodRabbit;
                break;
            case Rabbit.strange:
                music = Music.stangeRabbit;
                break;
            case Rabbit.sympathetic:
                music = Music.sympatheic;
                break;
        }
        otherMusic.clip = audioClip.First(a => a.music == music).audioClip;
        otherMusic.Play();
    }

    public void ResumeMainMusic()
    {
        otherMusic.Stop();
        mainMusic.Play();
    }

    //public void PlayOtherMusic(AudioClip )
}
