using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUIManager : MonoBehaviour
{
    StartUI startUI;
    private void Awake()
    {
        startUI = GetComponentInChildren<StartUI>(true);
        startUI.Init(this);
    }

    public void StartGame()
    {
        StartSceneManager.Instance.StartGame();
    }
}
