using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    public BoardManager boardManagerObj;

    public GameObject homeScreen;
    public GameObject levelScreen;
    public GameObject settingsScreen;
    public GameObject audioLibraryScreen;
    public GameObject pauseScreen;  
    public GameObject gameOverScreen;

    public GameObject hud;

    public GameObject dailyLoginPanel;

    public GameObject[] gameScreens;

    public AudioClip NewTrack;

    public AudioManager audioMan;


    void Start()
    {
        OpenScreen(homeScreen);
        audioMan = FindObjectOfType<AudioManager>();
        audioMan.ChangeMusic(NewTrack);
    }

    public void OpenScreen(GameObject currentScreen)
    {        
        for (int i = 0; i < gameScreens.Length; i++)
        {
            gameScreens[i].SetActive(false);
        }
        currentScreen.SetActive(true);
        
    }
    
    public void HomeScreen()
    {
        OpenScreen(homeScreen);
        boardManagerObj.ClearBoard();
    }

    public void LevelScreen()
    {
        OpenScreen(levelScreen);
        boardManagerObj.ClearBoard();
    }

    public void SetttingsScreen()
    {
        OpenScreen(settingsScreen);
    }

    public void AudioLibraryScreen()
    {
        OpenScreen(audioLibraryScreen);
    }

    public void PauseScreen()
    {
        OpenScreen(pauseScreen);
    }

    public void GameOverScreen()
    {
        OpenScreen(gameOverScreen);
    }    

    public void TestLevel()
    {
        boardManagerObj.NewGame();
        levelScreen.SetActive(false);
        hud.SetActive(true);
    }

    public void ResetLevel()
    {
        boardManagerObj.ClearBoard();
        boardManagerObj.NewGame();
        gameOverScreen.SetActive(false);        
    }    
}
