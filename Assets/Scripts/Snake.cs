using System;
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
    private enum State {
        Alive,
        Dead
    }

    private class SnakeBodyPart {
        private Transform transform;
        private SnakeMovePosition snakeMovePosition;

        public SnakeBodyPart(int bodyIndex) {
            GameObject snakeBodyPartGameObject = new GameObject("Snake Body", typeof(SpriteRenderer));
            SpriteRenderer snakeBodyPartSpriteRenderer = snakeBodyPartGameObject.GetComponent<SpriteRenderer>();
            snakeBodyPartSpriteRenderer.sprite = GameAssets.Instance.snakeBodySprite;
            snakeBodyPartSpriteRenderer.sortingOrder = -bodyIndex;
            transform = snakeBodyPartGameObject.transform;
        }

        public void SetMovePosition(SnakeMovePosition snakeMovePosition) {
            this.snakeMovePosition = snakeMovePosition;
            Vector2Int gridPosition = snakeMovePosition.GetGridPosition();
            transform.position = new Vector3(gridPosition.x, gridPosition.y, 0);

            //Angles
            float angle;
            switch (snakeMovePosition.GetDirection()) {
                default:
                case Direction.Left: // Currently Going Left
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default: // Previously Going Left
                            angle = 90;
                            break;
                        case Direction.Down: // Previously Going Down
                            angle = -45;
                            break;
                        case Direction.Up: // Previously Going Up
                            angle = 45;
                            break;
                    }
                    break;
                case Direction.Right: // Currently Going Right
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default: // Previously Going Right
                            angle = -90;
                            break;
                        case Direction.Down: // Previously Going Down
                            angle = 45;
                            break;
                        case Direction.Up: // Previously Going Up
                            angle = -45;
                            break;
                    }
                    break;
                case Direction.Up: // Currently Going Up
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default: // Previously Going Up
                            angle = 0;
                            break;
                        case Direction.Left: // Previously Going Left
                            angle = 45;
                            break;
                        case Direction.Right: // Previously Going Right
                            angle = -45;
                            break;
                    }
                    break;
                case Direction.Down: // Currently Going Down
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default: // Previously Going Down
                            angle = 180;
                            break;
                        case Direction.Left: // Previously Going Left
                            angle = -45;
                            break;
                        case Direction.Right: // Previously Going Right
                            angle = 45;
                            break;
                    }
                    break;
            }
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    private class SnakeMovePosition {

        private SnakeMovePosition previousSnakeMovePosition;
        private Direction direction;
        private Vector2Int gridPosition;

        public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector2Int gridPosition, Direction direction) {
            this.gridPosition = gridPosition;
            this.direction = direction;
            this.previousSnakeMovePosition = previousSnakeMovePosition;
        }

        public Vector2Int GetGridPosition() {
            return this.gridPosition;
        }

        public Direction GetDirection() {
            return this.direction;
        }

        public Direction GetPreviousDirection() {
            if (previousSnakeMovePosition == null) {
                return Direction.Right;
            }
            return this.previousSnakeMovePosition.GetDirection();
        }
    }

    private Vector2Int gridPosition;
    private Vector2Int startGridPosition;
    private Direction gridMoveDirection;

    private float verticalInput, horizontalInput;

    private float gridMoveTimer;
    private float gridMoveTimerMax = 0.15f;

    private LevelGrid levelGrid;
    private Food food;

    private bool hasInput = false; //Check if there is already an input
    
    private int snakeBodySize;
    private List<SnakeMovePosition> movePositionList;
    private List<SnakeBodyPart> snakeBodyPartsList;

    private State state;

    public static event EventHandler OnSnakeEat;

    void Awake()
    {
        startGridPosition = new Vector2Int(0, 0);
        gridPosition = startGridPosition;
        gridMoveDirection = Direction.Up;

        transform.eulerAngles = Vector3.zero;

        snakeBodySize = 0;
        movePositionList = new List<SnakeMovePosition>();
        snakeBodyPartsList = new List<SnakeBodyPart>();

        state = State.Alive;
    }

    void Update()
    {
        switch (state)
        {
            case State.Alive:
                HandleMoveDirection();
                HandleGridMovement();
                break;
            case State.Dead:
                break;
        }

    }

    public void Setup(LevelGrid levelGrid) {
        this.levelGrid = levelGrid;
    }

    public void Setup(Food food)
    {
        this.food = food;
    }

    private void HandleGridMovement() {
        gridMoveTimer += Time.deltaTime;

        //Check if timer surpassed the max timer
        if (gridMoveTimer >= gridMoveTimerMax)
        {            
            gridMoveTimer -= gridMoveTimerMax; //Reset timer

            SnakeMovePosition previousSnakeMovePosition = null;
            if (movePositionList.Count > 0) {
                previousSnakeMovePosition = movePositionList[0];
            }


            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, gridPosition,gridMoveDirection);
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
            gridPosition = levelGrid.ValidateGridPosition(gridPosition); //Check if out of bounds

            // Does Snake eat food?
            bool snakeAteFood = food.TrySnakeEatFood(gridPosition);
            if (snakeAteFood)
            {
                // GrowBody
                SoundManager.PlaySound(SoundManager.Sound.SnakeEat);
                snakeBodySize++;
                CreateBodyPart();
                if (OnSnakeEat != null)
                {
                   OnSnakeEat(null, EventArgs.Empty);
                }
            }

            if (movePositionList.Count > snakeBodySize) {
                movePositionList.RemoveAt(movePositionList.Count - 1);
            }


            //Check Game Over here because the posicion
            foreach (SnakeMovePosition move in movePositionList) {
                if (gridPosition == move.GetGridPosition()) {
                    state = State.Dead;
                    GameManager.Instance.SnakeDied();
                }
            }

            transform.position = new Vector3(gridPosition.x, gridPosition.y, 0);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirectionVector));
            SoundManager.PlaySound(SoundManager.Sound.SnakeMove);

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
        {
            gridPositionList.Add(snakeMove.GetGridPosition());
        }
                  
        return gridPositionList;
    }

    private void CreateBodyPart() {
        snakeBodyPartsList.Add(new SnakeBodyPart(snakeBodySize));    
    }

    private void UpdateBodyPart()
    {
        for (int i = 0; i < snakeBodyPartsList.Count; i++)
        {
            snakeBodyPartsList[i].SetMovePosition(movePositionList[i]);
        }
    }
}
