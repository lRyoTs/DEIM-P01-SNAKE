using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectModeUI : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Toggle invisibleFoodToggle;

    private void Awake()
    {
        startGameButton.onClick.AddListener(() => { 
            Loader.Load(Loader.Scene.Game);
            DataPersistence.sharedInstance.SetMode(invisibleFoodToggle.isOn);
        });
    }
}
