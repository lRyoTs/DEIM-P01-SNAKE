using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistence : MonoBehaviour
{
    public const int NORMAL_MODE = 0;
    public const int SPECIAL_MODE = 1;
    public static DataPersistence sharedInstance;
    private int mode; //[0] normal [1] special


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

    public void SetMode(int selectedMode)
    {
        mode = selectedMode;
    }

    public int GetMode() {
        return mode;
    }
}
