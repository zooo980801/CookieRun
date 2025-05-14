using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinItem : Item
{
    public float value = 12f;
    [SerializeField]
    private SpriteRenderer _renderer;
    [SerializeField]

    private TutorialManager tutorialManager;

    private GroundObjectData _data
    {
        get { return data; }
        set { data = value; }
    }
    private void Awake()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
    }
    public override void Initialize(GroundObjectData data)
    {
        this._data = data;
    }

    public void ApplyEffect()
    {
        GameManager.Instance.AddScore((int)value);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[CoinItem] Trigger with: {other.gameObject.name}");
        if (other.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name == "TutorialScene") //현재 튜토리얼씬일때
            {
                Debug.Log("듀토씬진입");
                if (tutorialManager != null && tutorialManager.IsCurrentStep(TutorialStep.CollectCoin) )
                {
                    Debug.Log("올라감");
                    tutorialManager.AdvanceStep();
                }
                
            }
            ApplyEffect();
            gameObject.SetActive(false);
        }
    }


}
