using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private bool isPaused = false;
    #endregion

    private void Awake()
    {
        if (Instance != null) {
            Debug.LogError("THere is more than Instance of GameManager");
        }
        Instance = this;

        Snake.OnSnakeEat += Snake_OnSnakeEat;
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

        SpawnFood(20,20);

        isPaused = false;

        Score.InitializedStaticScore();
        SoundManager.CreateSoundManagerGameobject();
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
        Snake.OnSnakeEat -= Snake_OnSnakeEat;
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

    private void SpawnFood(int width, int height)
    {
        Vector2Int foodGridPosition;
        do
        {
            foodGridPosition = new Vector2Int(Random.Range(-width / 2, width / 2), Random.Range(-height / 2, height / 2));
        } while (snake.GetSnakeFullBodyGridPosition().Contains(foodGridPosition));


        GameObject foodGameObject = new GameObject("Food");
        SpriteRenderer foodSpriteRenderer = foodGameObject.AddComponent<SpriteRenderer>();
        foodSpriteRenderer.sprite = GameAssets.Instance.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y, 0);
        Food food = foodGameObject.AddComponent<Food>();


        snake.Setup(food);
        food.Setup(snake);
    }

    private void Snake_OnSnakeEat(object sender, EventArgs e)
    {
        //What has to happen once Event is called
        SpawnFood(20,20);
    }
}
