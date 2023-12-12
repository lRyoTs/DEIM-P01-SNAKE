using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }
    [SerializeField] private GameObject foodGameObject;

    private LevelGrid levelGrid;
    private Snake snake;

    private void Awake()
    {
        if (Instance != null) {
            Debug.LogError("There is more than 1 instance of SpawnManager");
        }
        Instance = this;
        EventManager.AddHandler(EventManager.EVENT.OnSnakeEat,SpawnFood);
    }

    public void InitializeSpawnManager(LevelGrid levelGrid, Snake snake) {
        this.levelGrid = levelGrid;
        this.snake = snake;
        SpawnFood();
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(EventManager.EVENT.OnSnakeEat, SpawnFood);
    }

    public void SpawnFood() {
        Vector2Int foodGridPosition;
        do
        {
            foodGridPosition = new Vector2Int(Random.Range(-(levelGrid.GetWidth() / 2), levelGrid.GetWidth() / 2), Random.Range(-(levelGrid.GetHeight() / 2), levelGrid.GetHeight() / 2));
        } while (snake.GetSnakeFullBodyGridPosition().Contains(foodGridPosition));

        Food food = Instantiate(foodGameObject, new Vector3(foodGridPosition.x, foodGridPosition.y, 0),Quaternion.identity).GetComponent<Food>();

        snake.Setup(food);
    }
}
