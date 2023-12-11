using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }
    private LevelGrid levelGrid;
    private Snake snake;

    private void Awake()
    {
        if (Instance != null) {
            Debug.LogError("There is more than 1 instance of RecollectableSpawnManager");
        }
        Instance = this;
        EventManager.onSnakeEat += SpawnFood;
    }

    public void InitializeSpawnManager(LevelGrid levelGrid, Snake snake) {
        this.levelGrid = levelGrid;
        this.snake = snake;
        SpawnFood();
    }

    private void OnDisable()
    {
        EventManager.onSnakeEat -= SpawnFood;
    }

    public void SpawnFood() {
        Vector2Int foodGridPosition;
        do
        {
            foodGridPosition = new Vector2Int(Random.Range(-(levelGrid.GetWidth() / 2), levelGrid.GetWidth() / 2), Random.Range(-(levelGrid.GetHeight() / 2), levelGrid.GetHeight() / 2));
        } while (snake.GetSnakeFullBodyGridPosition().Contains(foodGridPosition));

        GameObject foodGameObject = new GameObject("Food");
        SpriteRenderer foodSpriteRenderer = foodGameObject.AddComponent<SpriteRenderer>();
        foodSpriteRenderer.sprite = GameAssets.Instance.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y, 0);
        Food food = foodGameObject.AddComponent<Food>();

        snake.Setup(food);
    }
}
