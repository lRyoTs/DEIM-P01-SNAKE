using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid
{
    private Vector2Int foodGridPosition;

    private int width;
    private int height;

    private GameObject foodGameObject;
    private Snake snake;

    public LevelGrid(int w, int h) {
        this.width = w;
        this.height = h;

        SpawnFood();
    }

    public void Setup(Snake snake)
    {
        this.snake = snake;
    }

    private void SpawnFood() {
        foodGridPosition = new Vector2Int(Random.Range(-width/2,width/2), Random.Range(-height/2,height/2));

        foodGameObject = new GameObject("Food");
        SpriteRenderer foodSpriteRenderer = foodGameObject.AddComponent<SpriteRenderer>();
        foodSpriteRenderer.sprite = GameAssets.Instance.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y, 0);
    }

    public void SnakeMoved(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == foodGridPosition)
        {
            Object.Destroy(foodGameObject);
            SpawnFood();
        }
    }
}
