using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistence : MonoBehaviour
{
    public static DataPersistence sharedInstance;
    private int fieldSize = 20;
    private bool mode;


    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void SetMode(bool selectedMode)
    {
        mode = selectedMode;
    }

    public bool GetMode() {
        return mode;
    }

    public void SetFieldSize(int fieldSize) {
        this.fieldSize = fieldSize;
    }

    public int GetFieldSize() {
        return fieldSize;
    }
}
