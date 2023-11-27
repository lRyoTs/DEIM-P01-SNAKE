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
        if(sharedInstance != null){
            Debug.Log("There is more than 1 instance of DataPersistence");
        }

        sharedInstance = this;
        DontDestroyOnLoad(this);
    }

    public void SetMode(int selectedMode)
    {
        mode = selectedMode;
    }

    public int GetMode() {
        return mode;
    }
}
