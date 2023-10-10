using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    private Vector2Int gridPosition;
    private Vector2Int startGridPosition;
    private Vector2Int gridMoveDirection;

    private float verticalInput, horizontalInput;
    private float gridMoveTimer;
    private float gridMoveTimerMax = 0.35f;

    private LevelGrid levelGrid;

    private bool hasInput = false; //Check if there is already an input

    void Awake()
    {
        startGridPosition = new Vector2Int(0, 0);
        gridPosition = startGridPosition;
        gridMoveDirection = new Vector2Int(0, 1);

        transform.eulerAngles = Vector3.zero;
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
            gridPosition += gridMoveDirection;
            gridMoveTimer -= gridMoveTimerMax; //Reset timer

            transform.position = new Vector3(gridPosition.x, gridPosition.y, 0);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection));

            hasInput = false;
        }

 
    }
    private void HandleMoveDirection()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        

        //Pressed W
        if ((verticalInput > 0) && !hasInput)
        {
            if (gridMoveDirection.x != 0) // Check if its going horizontal
            {
                //Change direction to UP
                gridMoveDirection.x = 0;
                gridMoveDirection.y = 1;
                hasInput = true;
            }
        }

        //Pressed S
        if ((verticalInput < 0) && !hasInput)
        {
            if (gridMoveDirection.x != 0)
            {
                //Change direction to DOWN
                gridMoveDirection.x = 0;
                gridMoveDirection.y = -1;
                hasInput = true;
            }
        }

        //Pressed D
        if ((horizontalInput > 0) && !hasInput)
        {
            if (gridMoveDirection.y != 0)
            {
                //Change direction to RIGHT
                gridMoveDirection.x = 1;
                gridMoveDirection.y = 0;
                hasInput = true;
            }

        }

        //Pressed A
        if ((horizontalInput < 0) && !hasInput)
        {
            if (gridMoveDirection.y != 0)
            {
                //Change direction to LEFT    
                gridMoveDirection.x = -1;
                gridMoveDirection.y = 0;
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
}
