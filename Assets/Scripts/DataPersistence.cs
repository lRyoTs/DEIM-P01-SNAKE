using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistence : MonoBehaviour
{
    public static DataPersistence sharedInstance;
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
}
