using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;

    public List<SoundClip> soundClips = new List<SoundClip>();

    private void Awake()
    {
        Instance = this;
    }

    public void PlayClip(string name)
    {
        foreach (SoundClip sc in soundClips)
        {
            if (sc.name == name)
            {
                audioSource.PlayOneShot(sc.audioClip);
            }
        }
    }

    public void ChangeMusic(AudioClip music)
    {
        audioSource.Stop();
        audioSource.clip = music;
        audioSource.Play();
    }
}

[System.Serializable]
public class SoundClip
{
    public string name;
    public AudioClip audioClip;
}

