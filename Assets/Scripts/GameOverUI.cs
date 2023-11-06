using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{

    public static GameOverUI Instance { get; private set; }

    [SerializeField] private Button restartButton;

    private void Awake()
    {
        if (Instance != null) {
            Debug.LogError("There is more than 1 instance of GameOverUI");
        }
        Instance = this;

        restartButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.Game); });

        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
