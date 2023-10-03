using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets Instance { get; private set; }

    public Sprite SnakeHeadSprite;

    private void Awake()
    {
        if (Instance != null) 
        {
            Debug.LogError("There is more than one Instance");
        }

        Instance = this;
    }
}
