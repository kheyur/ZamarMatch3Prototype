using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MediaPlayer : MonoBehaviour
{
    public SoundLibrary soundLibraryObj;

    public GameObject nextBtn;
    public GameObject prevBtn;
    public GameObject playBtn;
    public GameObject pauseBtn;
    public GameObject recordSpin;

    public Text currentSong;

    string song;

    public bool songPaused = false;

    bool songPlaying = false;

    int counter = 0;
    
    private float lastSpun;
    private float spinSpeed = 100;

    private void Start()
    {
        playBtn.SetActive(true);
        pauseBtn.SetActive(false);
    }

    public void NextSong()
    {
        if (counter < 19)
        {
            counter++;
        }

        QueueSong();
    }

    public void PrevSong()
    {
        if (counter > 0)
        {
            counter--;
        }

        QueueSong();
    }

    public void PauseSong()
    {
        songPlaying = false;
        soundLibraryObj.audioSource.Pause();
        pauseBtn.SetActive(false);
        playBtn.SetActive(true);
        songPaused = true;
        
    }

    public void PlaySong()
    {
        songPlaying = true;
        QueueSong();
        playBtn.SetActive(false);
        pauseBtn.SetActive(true);
        songPaused = false;
        
    }

    public void QueueSong()
    {
        soundLibraryObj.StopClip();
        soundLibraryObj.PlayClip(counter);
        song = soundLibraryObj.soundClips[counter].name;
        currentSong.text = song;
    }

    
    public void FixedUpdate()
    {
        if (songPlaying == true)
        {
            if (Time.time - lastSpun > 1 / spinSpeed)
            {
                lastSpun = Time.time;

                recordSpin.transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z + 1);
            }
        }        
    }
}
