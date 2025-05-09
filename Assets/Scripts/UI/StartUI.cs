using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    private StartUIManager startUiManager;
    public void Init(StartUIManager startUiManager)
    {
        this.startUiManager = startUiManager;
        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }
    public void OnClickStartButton()
    {
        startUiManager.StartGame();
    }

    // 종료 버튼 클릭 시 애플리케이션 종료
    public void OnClickExitButton()
    {
        Application.Quit();
    }
}
