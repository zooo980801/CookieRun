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
        optionUI = GetComponentInChildren<OptionUI>(true);
        
    }
    private void Start()
    {
        startUI.Init(this);
        optionUI.Init(this); 
    }
    public void StartGame()
    {
        StartSceneManager.Instance.StartGame();
    }
    public void StartTutorial()
    {
        StartSceneManager.Instance.StartTutorial();
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
