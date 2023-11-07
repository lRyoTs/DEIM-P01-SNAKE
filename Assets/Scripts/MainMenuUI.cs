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
        playButton.onClick.AddListener(() => { 
            Loader.Load(Loader.Scene.Game); 
            SoundManager.PlaySound(SoundManager.Sound.ButtonClick); 
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

        HideInstructionPanel();

        SoundManager.CreateSoundManagerGameobject();
    }

    private void ShowInstructionPanel() {
        instructionPanel.SetActive(true);
    }

    private void HideInstructionPanel() {
        instructionPanel.SetActive(false);
    }
}
