using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI coinText;

    private int coinCount;
    public GameObject gameOverPanel;
    GameUI gameUI;
    GameOverUI gameOverUI;
    PauseUI pauseUI;
    private UIState currentState;

    private void Awake()
    {
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI.Init(this);
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI.Init(this);
        pauseUI = GetComponentInChildren<PauseUI>(true);
        pauseUI.Init(this);


        ChangeState(UIState.Game);
    }

    public void UpdateScore(int score,int bestScore,int coinCount)
    {
        scoreText.text = score.ToString();
        bestScoreText.text = bestScore.ToString();
        coinText.text = coinCount.ToString();
    }
    public void SetGameOver()
    {
        gameOverUI.UpdateScore();
        ChangeState(UIState.GameOver);
    }
    public void ChangeState(UIState state)
    {
        currentState = state;

        gameUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
    }
    public void ShowPauseUI()
    {
        GameManager.Instance.PauseGame();
        if(pauseUI == null)
        {
            Debug.LogError("연결안됨");
        }
        pauseUI.gameObject.SetActive(true);
        pauseUI.UpdateScore();
    }

    public void HidePauseUI()
    {
        GameManager.Instance.ResumeGame();
        pauseUI.gameObject.SetActive(false);
    }

}
public enum UIState
{
    Game,
    GameOver,
    Pause
}