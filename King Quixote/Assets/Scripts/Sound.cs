using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound 
{
    //name, audioclip, [range(min,max)] volu,e & pitch, loop, [HideInInspector] audiosource
    public string name;

    public AudioClip clip;

    [Range(0.0f, 1.0f)]
    public float volume;
    [Range(0.1f, 3.0f)]
    public float pitch;

    public bool loop = false;

    [HideInInspector]
    public bool playing = false;

    [HideInInspector]
    public AudioSource source;
}
