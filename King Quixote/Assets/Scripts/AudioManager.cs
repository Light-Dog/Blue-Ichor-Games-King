using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    bool deaf = false;
    bool mute = false;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = false;
            s.source.Stop();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        print("Level index = " + levelIndex);

        switch (levelIndex)
        {
            case 0:
                FindObjectOfType<AudioManager>().Play("Menu");
                break;
            case 1:
                FindObjectOfType<AudioManager>().Play("Level1");
                break;
            case 2:
                FindObjectOfType<AudioManager>().Play("Level1");
                break;
            case 3:
                FindObjectOfType<AudioManager>().Play("Level3");
                break;
            case 5:
                FindObjectOfType<AudioManager>().Play("Death");
                break;
            default:
                FindObjectOfType<AudioManager>().Play("Menu");
                break;
        }
    }

    public void Deafen(bool toDeaf)
    {
        deaf = toDeaf;
        float multiplier = 4f;
        if (deaf)
            multiplier = .25f;

        AudioSource[] sources = gameObject.GetComponents<AudioSource>();
        foreach(AudioSource s in sources)
        {
            s.volume *= multiplier;
        }
    }

    public void StopSongs()
    {
        print("Stop all Songs");
        foreach (Sound s in sounds)
            s.playing = false;

        AudioSource[] sources = gameObject.GetComponents<AudioSource>();
        foreach (AudioSource s in sources)
        {
            s.Stop();
        }
    }

    public bool MuteSongs()
    {
        mute = !mute;

        AudioSource[] sources = gameObject.GetComponents<AudioSource>();
        foreach (AudioSource s in sources)
        {
            if (mute)
                s.Pause();
            else
                s.UnPause();
        }

        return mute;
    }

    public void SwitchSongs(string nameToSwtich)
    {
        if (checkActiveSong(nameToSwtich))
        {
            print("Song already Playing");
            return;
        }

        StopSongs();
        Play(nameToSwtich);
    }

    public bool checkActiveSong(string checkName)
    {
        print("Check for song " + checkName);
        Sound s = Array.Find(sounds, sound => sound.name == checkName);

        if (s.playing)
            return true;
        else
            return false;
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s != null)
        {
            s.source.Play();
            s.playing = true;
        }

    }
}
