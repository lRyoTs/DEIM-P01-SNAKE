using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button quitButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button instructionButton;
    [SerializeField] private Button closeInstructionButton;

    [SerializeField] private GameObject instructionPanel;

    private void Awake()
    {
        playButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.Game); });
        instructionButton.onClick.AddListener(ShowInstructionPanel);
        quitButton.onClick.AddListener(Application.Quit);
        closeInstructionButton.onClick.AddListener(HideInstructionPanel);

        HideInstructionPanel();
    }

    private void ShowInstructionPanel() {
    
    }

    private void HideInstructionPanel() {

    }
}
