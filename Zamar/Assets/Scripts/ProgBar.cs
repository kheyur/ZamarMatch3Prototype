using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgBar : MonoBehaviour
{
    public GUIManager gm;
    public float MaxScore = 3000 ;
    public float currentScore;
    public Image bar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = gm.score;
        bar.fillAmount = (currentScore / MaxScore);
    }
}
