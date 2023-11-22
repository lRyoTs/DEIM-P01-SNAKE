using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Score
{
    public static string HIGH_SCORE = "highScore"; //Key in PlayerPrefs
    public const int POINTS_TO_ADD = 100;
    private static int score; // Player score

    public static event EventHandler OnHighScoreChange;
    public static int GetHighScore() {
        return PlayerPrefs.GetInt(HIGH_SCORE,0);
    }

    public static bool TrySetNewHighScore()
    {
        int highScore = GetHighScore();
        if (score > highScore) {
            //Set new HighScore
            PlayerPrefs.SetInt(HIGH_SCORE, score);
            PlayerPrefs.Save();

            if (OnHighScoreChange != null) {
                OnHighScoreChange(null, EventArgs.Empty);
            }
            
            return true;
        }

        return false;
    }
    public static void InitializedStaticScore() {
        score = 0;
        AddScore(0);
        ScoreUI.instance.UpdateHighScoreText();
    }

    public static int GetScore()
    {
        return score;
    }

    public static void AddScore(int pointsToAdd)
    {
        score += pointsToAdd;
        ScoreUI.instance.UpdateScoreText(score);
    }
}
