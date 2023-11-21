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
    }

    public void Setup(Snake snake)
    {
        this.snake = snake;
        //SpawnFood();
    }
    /*
    private void SpawnFood() {
        do { 
            foodGridPosition = new Vector2Int(Random.Range(-width / 2, width / 2), Random.Range(-height / 2, height / 2));
        } while (snake.GetSnakeFullBodyGridPosition().Contains(foodGridPosition));
        

        foodGameObject = new GameObject("Food");
        SpriteRenderer foodSpriteRenderer = foodGameObject.AddComponent<SpriteRenderer>();
        foodSpriteRenderer.sprite = GameAssets.Instance.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y, 0);
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
    */

    public Vector2Int ValidateGridPosition(Vector2Int gridPosition) {
        int w = Half(width);
        int h = Half(height);

        //Pass rigth limit
        if (gridPosition.x > w) {
            gridPosition.x = -w; //Spawn in Left limit
        }

        //Pass left limit
        if (gridPosition.x < -w) {
            gridPosition.x = w; //Spawn in Right limit
        }

        //Pass up limit
        if (gridPosition.y > h)
        {
            gridPosition.y = -h; //Spawn in Down limit
        }

        //Pass down limit
        if (gridPosition.y < -h)
        {
            gridPosition.y = h; //Spawn in Up limit
        }

        return gridPosition;
    }

    private int Half(int number) {
        return number / 2;
    }
}
