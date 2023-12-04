using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    #region CONSTANTS

    #endregion

    #region VARIABLES
    
    private LevelGrid levelGrid;
    private Snake snake;
    private Food food;

    private bool isPaused = false;

    private Coroutine runningCoroutine;
    #endregion

    private void Awake()
    {
        if (Instance != null) {
            Debug.LogError("THere is more than Instance of GameManager");
        }
        Instance = this;

        EventManager.OnFoodSpawn += Food_OnFoodSpawn;
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

    private void OnDisable()
    {
        EventManager.OnFoodSpawn -= Food_OnFoodSpawn;
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
        isPaused=false;
    }

    public void StartGame() {
        //Configuration Snake
        GameObject snakeHeadGameObject = new GameObject("Snake Head");
        SpriteRenderer snakeSpriteRenderer = snakeHeadGameObject.AddComponent<SpriteRenderer>();
        snakeSpriteRenderer.sprite = GameAssets.Instance.snakeHeadSprite;
        snake = snakeHeadGameObject.AddComponent<Snake>();

        //Configuration LevelGrid
        levelGrid = new LevelGrid(20,20);
        snake.Setup(levelGrid);
        levelGrid.Setup(snake);

        food = new Food(levelGrid);
        food.Setup(snake);

        isPaused = false;

        Score.InitializedStaticScore();
        SoundManager.CreateSoundManagerGameobject();
    }

    private void Food_OnFoodSpawn(object sender, EventArgs e) {
        snake.Setup(food);
        if (DataPersistence.sharedInstance.GetMode() >= DataPersistence.SPECIAL_MODE) {
            if (runningCoroutine != null) {
                StopCoroutine(runningCoroutine);
            }
            runningCoroutine = StartCoroutine(TimerToInvisible(Food.TIMER_TO_INVISIBLE));
        } 
    }

    private IEnumerator TimerToInvisible(float timer) {
        yield return new WaitForSeconds(timer);
        food.MakeInvisible();
    }
}
