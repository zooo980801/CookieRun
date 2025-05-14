using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [Header("버튼")]
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button tutoExitButton;
    [Header("점수")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI CoinScoreText;
    [SerializeField] private TextMeshProUGUI DistanceText;

    private int CoinScoreTextValue;
    private int DistanceTextValue;
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        restartButton.onClick.AddListener(OnClickRestartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
        tutoExitButton?.onClick.AddListener(OnClickExitButton);
    }

    // 다시 시작 버튼 클릭 시 현재 씬을 다시 로드 (게임 재시작)
    public void OnClickRestartButton()
    {
        GameManager.Instance.RestartGame();
    }

    public void UpdateScore()
    {
        scoreText.text = uimanager.scoreText.text;
        bestScoreText.text = uimanager.bestScoreText.text;
        CoinScoreTextValue = int.Parse(uimanager.coinText.text) * 10;
        CoinScoreText.text=CoinScoreTextValue.ToString();
        DistanceTextValue = int.Parse(uimanager.scoreText.text) - (int.Parse(uimanager.coinText.text) * 10);
        DistanceText.text = DistanceTextValue.ToString();
    }
    // 종료 버튼 클릭 시 애플리케이션 종료
    public void OnClickExitButton()
    {
        Time.timeScale = 1f;
        SceneLoader.LoadScene("StartScene", "ExitFromPause");
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }

}
