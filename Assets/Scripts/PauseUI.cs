using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public static PauseUI instance { get; private set; }
    [SerializeField] private Button continueButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        if (instance != null) {
            Debug.LogError("There is more than 1 instance of PauseUI");
        }
        instance = this;

        mainMenuButton.onClick.AddListener(() => { 
            Time.timeScale = 1f; 
            Loader.Load(Loader.Scene.MainMenu);
        });

        continueButton.onClick.AddListener(() => { GameManager.Instance.ResumeGame(); });
        
        Hide();
    }

    public void SnakeDied()
    {
        GameOverUI.Instance.Show();
    }

    public void Show()
    {
        gameObject.SetActive(true);    
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
