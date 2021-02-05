using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLibrary : MonoBehaviour
{

    public static SoundLibrary Instance;

    public AudioSource audioSource;
    public List<SoundClips> soundClips = new List<SoundClips>();

    [System.Serializable]
    public class SoundClips
    {
        public string name;
        public AudioClip audioClip;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void PlayClip(int songID)
    {
        string songName = soundClips[songID].name;
        foreach (SoundClips sc in soundClips)
        {
            if (sc.name == songName)
            {
                audioSource.PlayOneShot(sc.audioClip);
            }
        }
    }

    public void StopClip()
    {
        audioSource.Stop();
    }
    public void ChangeMusic(AudioClip music)
    {
        audioSource.Stop();
        audioSource.clip = music;
        audioSource.Play();
    }    
}



