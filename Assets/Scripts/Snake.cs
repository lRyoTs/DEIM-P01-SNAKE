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
    private float gridMoveTimerMax = 1f;

    void Start()
    {
        startGridPosition = new Vector2Int(0, 0);
        gridPosition = startGridPosition;
        gridMoveDirection = new Vector2Int(0, 1);
    }

    void Update()
    {

        gridMoveTimer += Time.deltaTime;
        HandleMoveDirection();
        //Check if timer surpassed the max timer
        if (gridMoveTimer >= gridMoveTimerMax) {

            gridPosition += gridMoveDirection;
            gridMoveTimer -= gridMoveTimerMax; //Reset timer

            transform.position = new Vector3(gridPosition.x, gridPosition.y, 0);
        }
    }

    private void HandleMoveDirection()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //Pressed W
        if (verticalInput > 0) 
        {
            if (gridMoveDirection.x != 0) // Check if its going horizontal
            {
                //Change direction to UP
                gridMoveDirection.x = 0;
                gridMoveDirection.y = 1;
            }
        }

        //Pressed S
        if (verticalInput < 0) {
            
            if (gridMoveDirection.x != 0) 
            {
                //Change direction to DOWN
                gridMoveDirection.x = 0;
                gridMoveDirection.y = -1;
            }
        }

        //Pressed D
        if (horizontalInput > 0) {

            if(gridMoveDirection.y != 0) 
            {
                //Change direction to RIGHT
                gridMoveDirection.x = 1;
                gridMoveDirection.y = 0;
            }

        }

        //Pressed A
        if(horizontalInput < 0)
        {
            if (gridMoveDirection.y != 0) 
            {
                //Change direction to LEFT    
                gridMoveDirection.x = -1;
                gridMoveDirection.y = 0;
            }
        }
    
    }
}
