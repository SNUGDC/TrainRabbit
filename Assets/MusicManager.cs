using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static GameObject instance = null;

    public AudioSource mainMusic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
	}

    void Start()
    {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeMusic(AudioClip music)
    {
        mainMusic.Stop();
        mainMusic.clip = music;
        mainMusic.Play();
    }
}
