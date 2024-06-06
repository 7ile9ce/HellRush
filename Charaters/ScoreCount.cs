using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


public class ScoreCount : MonoBehaviour
{

    public static ScoreCount instance;
    public Text score, highScore;
    public int scoreCounter, highScoreCounter;

     private void Awake()
    {
        instance = this;

        if (PlayerPrefs.HasKey("SaveScore"))
        {
            highScoreCounter = PlayerPrefs.GetInt("SaveScore");
        }
        
    }

    void Start()
    {
        
    }


    void Update()
    {
        score.text = "Score: " + scoreCounter;
        highScore.text = "HighScore: " + highScoreCounter;

    }

    public void AddScore(int value)
    {
        scoreCounter+=value;

        HighScore();
    }
     public void HighScore()
    {
        if (scoreCounter > highScoreCounter)
        {
            highScoreCounter = scoreCounter;

            PlayerPrefs.SetInt("SaveScore", highScoreCounter);
        }
        
    }
    public void ResetScore()
    {
        scoreCounter = 0;
        score.text = "Score: " + scoreCounter;
    }
}
