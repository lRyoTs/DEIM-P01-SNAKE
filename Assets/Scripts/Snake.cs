using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private enum Direction {
        Up, Down, Left, Right
    }

    private class SnakeBodyPart {
        private Transform transform;
        private Vector2Int gridPosition;

        public SnakeBodyPart(int bodyIndex) {
            GameObject snakeBodyPartGameObject = new GameObject("Snake Body", typeof(SpriteRenderer));
            SpriteRenderer snakeBodyPartSpriteRenderer = snakeBodyPartGameObject.GetComponent<SpriteRenderer>();
            snakeBodyPartSpriteRenderer.sprite = GameAssets.Instance.snakeBodySprite;
            snakeBodyPartSpriteRenderer.sortingOrder = -bodyIndex;
            transform = snakeBodyPartGameObject.transform;
    
        }

        public void SetGridPosition(Vector2Int gridPosition) {
            this.gridPosition = gridPosition;
            transform.position = new Vector3(gridPosition.x, gridPosition.y, 0);
        }
    }

    private class SnakeMovePosition {
        private Direction direction;
        private Vector2Int gridPosition;

        public SnakeMovePosition(Vector2Int gridPosition, Direction direction) {
            this.gridPosition = gridPosition;
            this.direction = direction;
        }

        public Vector2Int GetGridPosition() {
            return this.gridPosition;
        }
    }

    private Vector2Int gridPosition;
    private Vector2Int startGridPosition;
    private Direction gridMoveDirection;

    private float verticalInput, horizontalInput;
    private float gridMoveTimer;
    private float gridMoveTimerMax = 0.35f;

    private LevelGrid levelGrid;

    private bool hasInput = false; //Check if there is already an input
    private int snakeBodySize;
    private List<SnakeMovePosition> movePositionList;
    private List<SnakeBodyPart> snakeBodyPartsList;

    void Awake()
    {
        startGridPosition = new Vector2Int(0, 0);
        gridPosition = startGridPosition;
        gridMoveDirection = Direction.Up;

        transform.eulerAngles = Vector3.zero;

        snakeBodySize = 0;
        movePositionList = new List<SnakeMovePosition>();
        snakeBodyPartsList = new List<SnakeBodyPart>();
}

    void Update()
    {   
        HandleMoveDirection();
        HandleGridMovement();
    }

    public void Setup(LevelGrid levelGrid) {
        this.levelGrid = levelGrid;
    }

    private void HandleGridMovement() {
        gridMoveTimer += Time.deltaTime;

        //Check if timer surpassed the max timer
        if (gridMoveTimer >= gridMoveTimerMax)
        {            
            gridMoveTimer -= gridMoveTimerMax; //Reset timer
            
            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(gridPosition,gridMoveDirection);
            movePositionList.Insert(0, snakeMovePosition);

            Vector2Int gridMoveDirectionVector;
            switch (gridMoveDirection) {
                default:
                case Direction.Up:
                    gridMoveDirectionVector = new Vector2Int(0, 1);
                    break;
                case Direction.Down:
                    gridMoveDirectionVector = new Vector2Int(0, -1);
                    break;
                case Direction.Right:
                    gridMoveDirectionVector = new Vector2Int(1,0);
                    break;
                case Direction.Left:
                    gridMoveDirectionVector = new Vector2Int(-1,0);
                    break;
            }

            gridPosition += gridMoveDirectionVector;

            // Does Snake eat food?
            bool snakeAteFood = levelGrid.TrySnakeEatFood(gridPosition);
            if (snakeAteFood)
            {
                // GrowBody
                snakeBodySize++;
                CreateBodyPart();
            }

            if (movePositionList.Count > snakeBodySize) {
                movePositionList.RemoveAt(movePositionList.Count - 1);
            }


            transform.position = new Vector3(gridPosition.x, gridPosition.y, 0);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirectionVector));

            hasInput = false;

            UpdateBodyPart();    
        }
    }
    private void HandleMoveDirection()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //Pressed W
        if ((verticalInput > 0) && !hasInput)
        {
            if (gridMoveDirection != Direction.Down) // Check if its going horizontal
            {
                //Change direction to UP
                gridMoveDirection = Direction.Up;
                hasInput = true;
            }
        }

        //Pressed S
        if ((verticalInput < 0) && !hasInput)
        {
            if (gridMoveDirection != Direction.Up)
            {
                //Change direction to DOWN
                gridMoveDirection = Direction.Down;
                hasInput = true;
            }
        }

        //Pressed D
        if ((horizontalInput > 0) && !hasInput)
        {
            if (gridMoveDirection != Direction.Left)
            {
                //Change direction to RIGHT
                gridMoveDirection = Direction.Right;
                hasInput = true;
            }

        }

        //Pressed A
        if ((horizontalInput < 0) && !hasInput)
        {
            if (gridMoveDirection != Direction.Right)
            {
                //Change direction to LEFT    
                gridMoveDirection = Direction.Left;
                hasInput = true;
            }
        }
    }

    private float GetAngleFromVector(Vector2Int direction) {
        float degrees = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (degrees > 0) {
            degrees += 360;
        }

        return degrees -  90;
    }

    public Vector2Int GetGridPosition() {
        return gridPosition;
    }

    public List<Vector2Int> GetSnakeFullBodyGridPosition() {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() {gridPosition};
        foreach (var snakeMove in movePositionList)
        gridPositionList.Add(snakeMove.GetGridPosition());
        return gridPositionList;
    }

    private void CreateBodyPart() {
        snakeBodyPartsList.Add(new SnakeBodyPart(snakeBodySize));    
    }

    private void UpdateBodyPart()
    {
        for (int i = 0; i < snakeBodyPartsList.Count; i++)
        {
            snakeBodyPartsList[i].SetGridPosition(movePositionList[i].GetGridPosition());
        }
    }
}
