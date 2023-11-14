using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{

    public static GameOverUI Instance { get; private set; }

    [SerializeField] private Button restartButton;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance != null) {
            Debug.LogError("There is more than 1 instance of GameOverUI");
        }
        Instance = this;

        restartButton.onClick.AddListener(() => { 
            SoundManager.PlaySound(SoundManager.Sound.ButtonClick); 
            Loader.Load(Loader.Scene.Game); 
        });

        Hide();
    }

    public void Show(bool newHighScore)
    {
        UpdateScoreAndHighScore(newHighScore);
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdateScoreAndHighScore(bool newHighScore)
    {
        scoreText.text = Score.GetScore().ToString();
        highScoreText.text = Score.GetHighScore().ToString();
        messageText.text = newHighScore ? "Congratulations" : "Try again, Next time";
    }
}
