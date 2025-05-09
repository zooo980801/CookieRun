using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : BaseUI
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button exitButton;
    GameOverUI gameOverUI;
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        resumeButton.onClick.AddListener(OnClickResumeButton);
        retryButton.onClick.AddListener(OnClickRetryButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }
    public void OnClickResumeButton()
    {
        uimanager.HidePauseUI();
    }
    public void OnClickRetryButton()
    {
        GameManager.Instance.RestartGame();
    }
    public void OnClickExitButton()
    {
        gameOverUI.OnClickExitButton();
    }
    protected override UIState GetUIState()
    {
        return UIState.Pause;
    }
}
