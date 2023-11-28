using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[RequireComponent (typeof(SpriteRenderer))]
public class Food
{
    #region ATTRIBUTES
    private GameObject foodGameObject;
    private Vector2Int foodGridPosition;
    private SpriteRenderer foodSpriteRenderer;
    #endregion

    private Snake snake;
    private LevelGrid levelGrid;

    public  static event EventHandler OnFoodSpawn;

    public Food(LevelGrid levelGrid) {
        this.levelGrid = levelGrid;
    }

    public bool TrySnakeEatFood(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == foodGridPosition)
        {
            Object.Destroy(foodGameObject);
            SpawnFood();
            Score.AddScore(Score.POINTS_TO_ADD); //Increase score
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MakeInvisible() {
        foodSpriteRenderer.color = Color.clear;
    }

    public void Setup(Snake snake)
    {
        this.snake = snake;
        SpawnFood();
    }

    public void Setup(LevelGrid levelGrid) {
        this.levelGrid = levelGrid;
    }
    
    private void SpawnFood()
    {
        do
        {
            foodGridPosition = new Vector2Int(Random.Range(-(levelGrid.GetWidth() / 2), levelGrid.GetWidth() / 2), Random.Range(-(levelGrid.GetHeight() / 2), levelGrid.GetHeight() / 2));
        } while (snake.GetSnakeFullBodyGridPosition().Contains(foodGridPosition));

        foodGameObject = new GameObject("Food");
        foodSpriteRenderer = foodGameObject.AddComponent<SpriteRenderer>();
        foodSpriteRenderer.sprite = GameAssets.Instance.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y, 0);

        if (OnFoodSpawn != null) {
            OnFoodSpawn(null, EventArgs.Empty);
        }
    }
}
