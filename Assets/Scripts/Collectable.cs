using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    protected Vector2Int recollectableGridPosition;

    public abstract bool TrySnakeEat(Vector2Int snakeGridPosition);
}
