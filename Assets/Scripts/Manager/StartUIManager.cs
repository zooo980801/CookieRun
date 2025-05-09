using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartUIManager : MonoBehaviour
{
    StartUI startUI;
    OptionUI optionUI;
    private void Awake()
    {
        startUI = GetComponentInChildren<StartUI>(true);
        startUI.Init(this);
        optionUI = GetComponentInChildren<OptionUI>(true);
        optionUI.Init(this);
    }

    public void StartGame()
    {
        StartSceneManager.Instance.StartGame();
    }

    public void ShowOptionUI()
    {
        optionUI.gameObject.SetActive(true);
    }
    public void HideOptionUI()
    {
        optionUI.gameObject.SetActive(false);
    }
}
