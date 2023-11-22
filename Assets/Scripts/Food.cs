using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class Food : MonoBehaviour
{
    private Vector2Int foodGridPosition;
    private SpriteRenderer foodSpriteRenderer;
    private Snake snake;

    private void Awake()
    {
        foodSpriteRenderer = GetComponent<SpriteRenderer>();
        
        Invoke("MakeInvisible", 2f);
    }

    private void Start()
    {
        foodGridPosition = new Vector2Int((int)transform.position.x,(int)transform.position.y);
    }

    public bool TrySnakeEatFood(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == foodGridPosition)
        {
            Object.Destroy(gameObject);
            //SpawnFood();
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
    }

    /*
    private void SpawnFood(int width, int height)
    {
        do
        {
            foodGridPosition = new Vector2Int(Random.Range(-width / 2, width / 2), Random.Range(-height / 2, height / 2));
        } while (snake.GetSnakeFullBodyGridPosition().Contains(foodGridPosition));


        foodGameObject = new GameObject("Food");
        SpriteRenderer foodSpriteRenderer = foodGameObject.AddComponent<SpriteRenderer>();
        foodSpriteRenderer.sprite = GameAssets.Instance.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y, 0);
    }
    */
}