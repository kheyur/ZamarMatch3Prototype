using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public bool notificationsOn = true;
    public GameObject notificationsOnBtn;

    public bool soundOn = true;
    public GameObject soundOnBtn;

    public bool musicOn = true;
    public GameObject musicOnBtn;          

    public void Notifications()
    {
        if (notificationsOn == true)
        {
            notificationsOn = false;
            notificationsOnBtn.SetActive(false);
        }
        else if (notificationsOn == false)
        {
            notificationsOn = true;
            notificationsOnBtn.SetActive(true);
        }
    }

    public void Sound()
    {
        if (soundOn == true)
        {
            soundOn = false;
            soundOnBtn.SetActive(false);
        }
        else if (soundOn == false)
        {
            soundOn = true;
            soundOnBtn.SetActive(true);
        }
    }

    public void Music()
    {
        if (musicOn == true)
        {
            musicOn = false;
            musicOnBtn.SetActive(false);
        }
        else if (musicOn == false)
        {
            musicOn = true;
            musicOnBtn.SetActive(true);
        }
    }
}
