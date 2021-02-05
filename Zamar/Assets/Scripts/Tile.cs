using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
	private static Tile previousSelected = null;

	private SpriteRenderer render;

    public AnimationCurve tileSwop;

    public Sprite tile1;
    public Sprite tile2;


	private bool isSelected = false;
    public bool gameOver = false;

	private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    
    public float animationtime = 0.5f;

    void Awake()
    {
		render = GetComponent<SpriteRenderer>();
    }

	private void Select()
    {
		isSelected = true;
		render.color = Color.grey;
		previousSelected = gameObject.GetComponent<Tile>();
        AudioManager.Instance.PlayClip("Select");
	}

	private void Deselect()
    {
		isSelected = false;
		render.color = Color.white;
		previousSelected = null;
        AudioManager.Instance.PlayClip("Deselect");
	}

	void OnMouseDown()
    {
		if (render.sprite == null || BoardManager.instance.IsShifting || gameOver)
        {
			return;
		}
		if (isSelected)
        { 
			Deselect();
		}
        else
        {
			if (previousSelected == null)
            { 
				Select();
			}
            else
            {
				if (GetAllAdjacentTiles().Contains(previousSelected.gameObject))
                {
                    GameObject tile1Clone = Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation);
                    GameObject tile2Clone = Instantiate(previousSelected.gameObject, previousSelected.gameObject.transform.position, previousSelected.gameObject.transform.rotation);

                    StartCoroutine(SwapAnimation(tile1Clone.gameObject, animationtime, tile1Clone.transform.position, tile2Clone.transform.position));
                    StartCoroutine(SwapAnimation(tile2Clone.gameObject, animationtime, tile2Clone.transform.position, tile1Clone.transform.position));

                    SwapSprite(previousSelected.render);
					previousSelected.ClearAllMatches();
					previousSelected.Deselect();
					ClearAllMatches();
				}
                else
                {
					previousSelected.GetComponent<Tile>().Deselect();
					Select();
				}
			}
		}
    }

    IEnumerator SwapAnimation(GameObject tileToAnimate, float duration, Vector3 startPos, Vector3 endPos)
    {
        float t = 0;

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            t = Time.deltaTime;
            tileToAnimate.transform.position = Vector3.Lerp(startPos, endPos, duration);
            elapsedTime += Time.deltaTime;
        }

        Destroy(tileToAnimate.gameObject, animationtime);

        yield return new WaitForEndOfFrame();
    }

    public void SwapSprite(SpriteRenderer render2)
    {
		if (render.sprite == render2.sprite)
        {

			return;
		}
        
		Sprite tempSprite = render2.sprite;
		render2.sprite = render.sprite;
		render.sprite = tempSprite;

        render.enabled = false;
        render2.enabled = enabled = false;

        StartCoroutine(renderAgain(animationtime, render));
        StartCoroutine(renderAgain(animationtime, render2));

        GUIManager.instance.MoveCounter--;

        AudioManager.Instance.PlayClip("Swap");
	}

    IEnumerator renderAgain(float wait, Renderer render)
    {
        yield return new WaitForSeconds(wait);
        render.enabled = true;
    }

	private List<GameObject> GetAllAdjacentTiles()
    {
		List<GameObject> adjacentTiles = new List<GameObject>();
        
        for (int i = 0; i < adjacentDirections.Length; i++)
        {
			adjacentTiles.Add(GetAdjacent(adjacentDirections[i]));  

		}
		return adjacentTiles;
	}
    
    private GameObject GetAdjacent(Vector2 castDir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    private List<GameObject> FindMatch(Vector2 castDir)
    {
		List<GameObject> matchingTiles = new List<GameObject>();
        
		RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
        
		while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite)
        {
			matchingTiles.Add(hit.collider.gameObject);
			hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
		}
		return matchingTiles;
	}

	private void ClearMatch(Vector2[] paths)
    {
		List<GameObject> matchingTiles = new List<GameObject>();
		for (int i = 0; i < paths.Length; i++)
        {
            matchingTiles.AddRange(FindMatch(paths[i]));
        }
		if (matchingTiles.Count >= 2)
        {
			for (int i = 0; i < matchingTiles.Count; i++)
            {
				matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
			}
			matchFound = true;
		}
	}

	private bool matchFound = false;

	public void ClearAllMatches()
    {
		if (render.sprite == null)
			return;

		ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
		ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });

		if (matchFound)
        {
            StartCoroutine(clearDelay(animationtime));
		}
	}

    IEnumerator clearDelay(float wait)
    {
        yield return new WaitForSeconds(wait);
        render.sprite = null;
        matchFound = false;
        StopCoroutine(BoardManager.instance.FindNullTiles()); 
        StartCoroutine(BoardManager.instance.FindNullTiles()); 
        AudioManager.Instance.PlayClip("Match");
    }

    private void OnEnable()
    {
        GUIManager.OnGameOver += OnGameOverHandler;
    }

    void OnGameOverHandler()
    {
        gameOver = true;
    }
}