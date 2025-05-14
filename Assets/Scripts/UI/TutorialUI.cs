using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public static TutorialUI Instance;
    public TextMeshProUGUI tutorialText;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowStep(TutorialStep step)
    {
        switch (step)
        {
            case TutorialStep.Jump:
                tutorialText.text = "왼쪽 점프 버튼을 누르거나 스페이스바를 눌러 점프하세요!";
                break;
            case TutorialStep.Slide:
                tutorialText.text = "오른쪽 슬라이드 키를 누르거나 Shift키를 눌러 슬라이드 하세요!";
                break;
            case TutorialStep.CollectCoin:
                tutorialText.text = "앞에 있는 코인을 먹어보세요! 10점 추가됩니다";
                break;
            case TutorialStep.TakeDamage:
                tutorialText.text = "적을 피하거나 부딪혀서 데미지를 받아보세요!";
                break;
            case TutorialStep.Complete:
                tutorialText.text = "튜토리얼 완료! 이제 게임을 즐겨보세요!";
                break;
        }
    }
}
