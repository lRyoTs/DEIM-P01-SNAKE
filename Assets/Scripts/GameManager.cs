using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    #region VARIABLES
    private LevelGrid levelGrid;
    private Snake snake;
    private Food food;

    private bool isPaused = false;

    private Coroutine runningCoroutine; //Store the current running coroutine
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
        StartGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
               PauseGame();
            }
        }
    }

    public void SnakeDied()
    {
        SoundManager.PlaySound(SoundManager.Sound.SnakeDie);
        GameOverUI.Instance.Show(Score.TrySetNewHighScore());
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        PauseUI.instance.Show();
        isPaused = true;
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        PauseUI.instance.Hide();
        isPaused = false;
    }

    public void StartGame() {
        //Configuration Snake
        GameObject snakeHeadGameObject = new GameObject("Snake Head");
        SpriteRenderer snakeSpriteRenderer = snakeHeadGameObject.AddComponent<SpriteRenderer>();
        snakeSpriteRenderer.sprite = GameAssets.Instance.snakeHeadSprite;
        snake = snakeHeadGameObject.AddComponent<Snake>();

        //Configuration LevelGrid
        levelGrid = new LevelGrid(DataPersistence.sharedInstance.GetFieldSize(),DataPersistence.sharedInstance.GetFieldSize());
        snake.Setup(levelGrid);

        SpawnManager.Instance.InitializeSpawnManager(levelGrid, snake);

        isPaused = false;

        Score.InitializedStaticScore();
        SoundManager.CreateSoundManagerGameobject();
    }
}
