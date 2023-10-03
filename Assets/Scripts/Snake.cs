using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    private Vector2Int gridPosition;
    private Vector2Int startGridPosition;
    private Vector2Int gridDirection;

    private float verticalInput, horizontalInput;
    private float gridTimer;
    private float gridTimerMax = 1f;
    // Start is called before the first frame update
    void Start()
    {
        startGridPosition = new Vector2Int(0, 0);
        gridPosition = startGridPosition;
        gridDirection = new Vector2Int(0, 1);
    }

    // Update is called once per frame
    void Update()
    {

        gridTimer += Time.deltaTime;
        //Check if timer surpassed the max timer
        if (gridTimer >= gridTimerMax) {

            gridPosition += gridDirection;
            gridTimer -= gridTimerMax; //Reset timer

            transform.position = new Vector3(gridPosition.x, gridPosition.y, 0);
        }
    }
}
