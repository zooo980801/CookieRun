using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    private StartUIManager startUiManager;
    public void Init(StartUIManager startUiManager)
    {
        this.startUiManager = startUiManager;
        exitButton.onClick.AddListener(OnClickExitButton);
    }
    public void OnClickExitButton()
    {
        startUiManager.HideOptionUI();
    }
}
