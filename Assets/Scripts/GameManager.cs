using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    #region CONSTANTS
    public const int POINTS_TO_ADD = 100;
    #endregion

    #region VARIABLES
    private int score; // Player score

    private LevelGrid levelGrid;
    private Snake snake;

    private ScoreUI scoreUI;
    #endregion

    private void Awake()
    {
        if (Instance != null) {
            Debug.LogError("THere is more than Instance of GameManager");
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Configuration Snake
        GameObject snakeHeadGameObject = new GameObject("Snake Head");
        SpriteRenderer snakeSpriteRenderer = snakeHeadGameObject.AddComponent<SpriteRenderer>();
        snakeSpriteRenderer.sprite = GameAssets.Instance.snakeHeadSprite;
        snake = snakeHeadGameObject.AddComponent<Snake>();

        //Configuration LevelGrid
        levelGrid = new LevelGrid(20,20);
        snake.Setup(levelGrid);
        levelGrid.Setup(snake);

        scoreUI = GetComponentInChildren<ScoreUI>();

        score = 0;
        AddScore(0);
    }

    public int GetScore() {
        return score;
    }

    public void AddScore(int pointsToAdd) {
        this.score += pointsToAdd;
        scoreUI.UpdateScoreText(score);
    }
}
