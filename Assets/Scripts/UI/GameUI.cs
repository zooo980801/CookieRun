using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button jumpButton;
    [SerializeField] private Button SlideButton;
    private bool pauseActive = false;
    private bool slideActive = false;
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        pauseButton.onClick.AddListener(OnClickPauseButton);
        jumpButton.onClick.AddListener(OnClickJumpButton);
        SlideButton.onClick.AddListener(OnClickSlideButton);
    }
    private void Start()
    {
        UpdateHPSlider(1); // 시작 시 체력 슬라이더를 가득 채움 (100%)
    }

    // 체력 슬라이더 값을 퍼센트(0~1)로 설정
    public void UpdateHPSlider(float percentage)
    {
        hpSlider.value = percentage;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseActive == false)
            {
                uimanager.ShowPauseUI();
                pauseActive = true;
            }
            else
            {
                uimanager.HidePauseUI();
                pauseActive = false;
            }
        }
        if (slideActive)
        {
            //슬라이드 구현
        }
    }
    public void OnClickPauseButton()
    {
        if (pauseActive == false)
        {
            uimanager.ShowPauseUI();
            pauseActive = true;
        }
        else
        {
            uimanager.HidePauseUI();
            pauseActive = false;
        }
    }
    public void OnClickJumpButton()
    {
        //여기에 캐릭터 점프 연동
    }
    public void OnClickSlideButton()
    {
        //여기에 캐릭터 슬라이드 연동
    }

    public void OnPointerDown()
    {
        slideActive = true;
    }

    public void OnPointerUp()
    {
        slideActive = false;
    }
    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
