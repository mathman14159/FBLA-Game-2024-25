using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter instance;
    public TMP_Text scoreText;
    public int currentScore;
    
    void Awake()
    {
        instance = this;
    }
    
    
    void Start()
    {
        currentScore = PlayerPrefs.GetInt("Score");
        scoreText.text = "" + currentScore.ToString();
    }

    public void IncreaseScore(int v)
    {
        currentScore += v;
        scoreText.text = "" + currentScore.ToString();
    }
}
