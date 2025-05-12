using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneFadeIn : MonoBehaviour
{
    public Image fadeImage; // Canvas 최상단 검정 이미지
    public float fadeDuration = 1f;

    void Start()
    {
        // "Pause → Start" 전환일 때만 페이드 인 실행
        if (SceneLoader.transitionReason == "ExitFromPause")
        {
            fadeImage.gameObject.SetActive(true);
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
        float t = 1f;
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        while (t > 0f)
        {
            t -= Time.deltaTime / fadeDuration;
            color.a = t;
            fadeImage.color = color;
            yield return null;
        }
        fadeImage.gameObject.SetActive(false);
    }
}
