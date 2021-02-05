using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

    public bool gameOver = false;
    
	public void Exit()
    {
        Application.Quit();
    }
}
