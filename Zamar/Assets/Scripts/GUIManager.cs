using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour
{
	public static GUIManager instance;

    public delegate void GameOverHandler();
    public static event GameOverHandler OnGameOver;

    public GameObject gameOverPanel;

	public Text scoreTxt;
	public Text moveCounterTxt;

	public int score, moveCounter;

	void Awake()
    {
		instance = GetComponent<GUIManager>();
		moveCounter = 20;
	}

    private void Start()
    {
        moveCounterTxt.text = moveCounter +  "";
        scoreTxt.text = 0 + "";
    }

    public void GameOver()
    {

		gameOverPanel.SetActive(true);

        OnGameOver();
	}

	public int Score
    {
		get
        {
			return score;
		}

		set
        {
			score = value;
			scoreTxt.text = score.ToString();
		}
	}

	public int MoveCounter
    {
		get
        {
			return moveCounter;
		}

		set
        {
			moveCounter = value;
			if (moveCounter <= 0)
            {
				moveCounter = 0;
				StartCoroutine(WaitForShifting());
			}
			moveCounterTxt.text = moveCounter.ToString();
		}
	}

	private IEnumerator WaitForShifting()
    {
		yield return new WaitUntil(() => !BoardManager.instance.IsShifting);
		yield return new WaitForSeconds(.25f);
		GameOver();
	}
}
