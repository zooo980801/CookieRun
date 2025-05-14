using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialStep
{
    Jump,
    Slide,
    CollectCoin,
    TakeDamage,
    Complete
}

public class TutorialManager : MonoBehaviour
{

    public bool IsStepLocked { get; private set; } = true;

    public static TutorialManager Instance;

    public TutorialStep currentStep;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Time.timeScale = 1f; // 게임 흐름은 정상 유지
        ShowCurrentStep(); // 시작 시 첫 말풍선 출력
    }

    public void AdvanceStep()
    {
        currentStep++;
        IsStepLocked = true;
        ShowCurrentStep();
        if (currentStep == TutorialStep.Complete)
        {
            OnTutorialComplete();
        }
    }

    public bool IsCurrentStep(TutorialStep step)
    {
        return currentStep == step;
    }
    public void UnlockStep()
    {
        IsStepLocked = false;
        Time.timeScale = 1f; // 게임 다시 재생
    }
    public void ShowCurrentStep()
    {
        Debug.Log("튜토리얼 다음 단계로: " + currentStep);
        TutorialUI.Instance?.ShowStep(currentStep);
    }
    private void OnTutorialComplete()
    {
        GameManager.Instance.PauseGame();
        TutorialUI.Instance?.gameObject.SetActive(false); // 말풍선 숨김
        FindObjectOfType<UIManager>().SetGameOver();       // GameOverUI 표시
    }
}
