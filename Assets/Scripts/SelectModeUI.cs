using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectModeUI : MonoBehaviour
{

    [SerializeField] private Button normalModeButton;
    [SerializeField] private Button specialModeButton;

    private void Awake()
    {
        normalModeButton.onClick.AddListener(() => { 
            Loader.Load(Loader.Scene.Game);
            DataPersistence.sharedInstance.SetMode(DataPersistence.NORMAL_MODE);
        });

        specialModeButton.onClick.AddListener(() => { 
            Loader.Load(Loader.Scene.Game);
            DataPersistence.sharedInstance.SetMode(DataPersistence.SPECIAL_MODE);
        });
    }
}
