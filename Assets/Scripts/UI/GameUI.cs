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

    public void UpdateHPSlider(float hp)
    {
        hpSlider.value = hp /100f;
    }
    private void Update()
    {
        UpdateHPSlider(playerController.CurrentHp);
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
        //���⿡ ĳ���� ���� ����
        if (playerController.isGrounded)
        {
            playerController.Jump();
        }
    }
    
    public void OnSlideButtonDown()
    {
        slideActive = true;
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
