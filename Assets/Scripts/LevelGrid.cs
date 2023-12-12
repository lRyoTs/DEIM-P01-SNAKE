using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid
{
    private int width;
    private int height;

    public LevelGrid(int w, int h) {
        this.width = w;
        this.height = h;
    }

    public int GetHeight() {
        return height;
    }

    public int GetWidth()
    {
        return width;
    }

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
