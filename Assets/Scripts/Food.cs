using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[RequireComponent (typeof(SpriteRenderer))]
public class Food
{
    private Vector2Int foodGridPosition;
    private SpriteRenderer foodSpriteRenderer;
    private Snake snake;
    private LevelGrid levelGrid;

    private GameObject foodGameObject;
    public  static event EventHandler OnFoodSpawn;

    public Food(LevelGrid levelGrid) {
        this.levelGrid = levelGrid;
    }

    public bool TrySnakeEatFood(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == foodGridPosition)
        {
            Object.Destroy(foodGameObject);
            SpawnFood(20,20);
            Score.AddScore(Score.POINTS_TO_ADD); //Increase score
            return true;
        }
        else
        {
            return false;
        }
    }

    private void MakeInvisible() {
        foodSpriteRenderer.color = Color.clear;
    }

    public void Setup(Snake snake)
    {
        this.snake = snake;
        SpawnFood(20,20);
    }

    public void Setup(LevelGrid levelGrid) {
        this.levelGrid = levelGrid;
    }
    
    private void SpawnFood(int width, int height)
    {
        do
        {
            foodGridPosition = new Vector2Int(Random.Range(-width / 2, width / 2), Random.Range(-height / 2, height / 2));
        } while (snake.GetSnakeFullBodyGridPosition().Contains(foodGridPosition));


        foodGameObject = new GameObject("Food");
        foodSpriteRenderer = foodGameObject.AddComponent<SpriteRenderer>();
        foodSpriteRenderer.sprite = GameAssets.Instance.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y, 0);

        if (OnFoodSpawn != null) {
            OnFoodSpawn(null, EventArgs.Empty);
        }
    }
    
    public IEnumerator CountDownToInvisible() {
        yield return new WaitForSeconds(2f);
        MakeInvisible();
    }
}
