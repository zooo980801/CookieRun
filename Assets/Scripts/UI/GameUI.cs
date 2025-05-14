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
    [SerializeField]private float scoreInterval = 1f;

    private float scoreTimer = 0f;

    public static GameUI Instance;

    [SerializeField] private PlayerController playerController;

    private bool pauseActive = false;
    private bool slideActive = false;


    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        pauseButton.onClick.AddListener(OnClickPauseButton);
        //jumpButton.onClick.AddListener(OnClickJumpButton);
        //SlideButton.onClick.AddListener(OnClickSlideButton);
    }
    private void Start()
    {
        UpdateHPSlider(1); 
    }
    private void Awake()
    {
        Instance = this;
    }

    public bool IsSlidePressed()
    {
        return slideActive;
    }
    public void UpdateHPSlider(float hp)
    {
        hpSlider.value = hp /100f;
    }
    private void Update()
    {

        UpdateHPSlider(playerController.CurrentHp);

        scoreTimer += Time.deltaTime;
        if (scoreTimer >= scoreInterval)
        {
            GameManager.Instance.AddScore(1); // 1점 추가
            scoreTimer = 0f;
        }

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
        
        playerController.Slide(slideActive);
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
    // public void OnClickJumpButton()
    // {
    //     //���⿡ ĳ���� ���� ����
    //     if (playerController.isGrounded)
    //     {
    //         playerController.Jump();
    //     }
    // }
    // public void OnClickSlideButton()
    // {
    //     //���⿡ ĳ���� �����̵� ����
    //     slideActive = true;
    // }

    public void OnJumpButtonDown()
    {
        playerController.Jump(); // 점프
        if (TutorialManager.Instance != null && TutorialManager.Instance.IsCurrentStep(TutorialStep.Jump))
        {
            TutorialManager.Instance.UnlockStep(); // 멈춤 해제
            TutorialManager.Instance.AdvanceStep();
        }
    }

    public void OnSlideButtonDown()
    {

        slideActive = true;

        if (TutorialManager.Instance != null && TutorialManager.Instance.IsCurrentStep(TutorialStep.Slide))
        {
            TutorialManager.Instance.UnlockStep();
            TutorialManager.Instance.AdvanceStep();
        }
    }


    public void OnSlideButtonUp()
    {
        slideActive = false;
    }







    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
