using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button exitButton;
    private StartUIManager startUiManager;
    public void Init(StartUIManager startUiManager)
    {
        this.startUiManager = startUiManager;
        startButton.onClick.AddListener(OnClickStartButton);
        optionButton.onClick.AddListener(OnClickOptionButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }
    public void OnClickStartButton()
    {
        startUiManager.StartGame();
    }
    public void OnClickOptionButton()
    {
        startUiManager.ShowOptionUI();
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }
}
