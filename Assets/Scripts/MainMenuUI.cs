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
    [SerializeField] private Button closeSelectButton;

    [SerializeField] private GameObject instructionPanel;
    [SerializeField] private GameObject selectPanel;

    private void Awake()
    {
        playButton.onClick.AddListener(() => { 
            SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
            ShowSelectPanel();
        });

        instructionButton.onClick.AddListener(() => { 
            SoundManager.PlaySound(SoundManager.Sound.ButtonClick); 
            ShowInstructionPanel(); 
        }) ;
        
        quitButton.onClick.AddListener(() => { 
            SoundManager.PlaySound(SoundManager.Sound.ButtonClick); 
            Application.Quit(); 
        });
        
        closeInstructionButton.onClick.AddListener(() => {
            SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
            HideInstructionPanel(); 
        });

        closeSelectButton.onClick.AddListener(() => {
            SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
            HideSelectPanel();
        });

        HideInstructionPanel();
        HideSelectPanel();

        SoundManager.CreateSoundManagerGameobject();
    }

    private void ShowInstructionPanel() {
        instructionPanel.SetActive(true);
    }

    private void HideInstructionPanel() {
        instructionPanel.SetActive(false);
    }

    private void ShowSelectPanel()
    {
        selectPanel.SetActive(true);
    }

    private void HideSelectPanel()
    {
        selectPanel.SetActive(false);
    }
}
