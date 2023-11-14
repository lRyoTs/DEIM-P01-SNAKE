using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreUI : MonoBehaviour
{
    public static ScoreUI instance { get; private set;}

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;


    private void Awake()
    {
        if (instance != null) {
            Debug.LogError("There is more than 1 instance of ScoreUI");
        }
        instance = this;

        Score.OnHighScoreChange += Score_OnHighScoreText;
    }
    public void UpdateScoreText(int score) {
        scoreText.text = "SCORE\n" + score.ToString();
    }

    public void UpdateHighScoreText()
    {
        int highScore = Score.GetHighScore();
        highScoreText.text = highScore.ToString();
    }

    private void OnDisable()
    {
        Score.OnHighScoreChange -= Score_OnHighScoreText;
    }

    private void Score_OnHighScoreText(object sender,EventArgs e) {
        //What has to happen once Event is called
        UpdateHighScoreText();
    }
}
