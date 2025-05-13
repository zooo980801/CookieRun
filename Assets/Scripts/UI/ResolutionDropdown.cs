using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionDropdown : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    void Start()
    {
        // 원하는 해상도 옵션 수동 입력
        List<string> options = new List<string> { "1920 x 1080" ,"1280 x 720"};

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

        // 현재 해상도와 일치하는 인덱스 설정
        int currentIndex = 0;
        if (Screen.width == 1920 && Screen.height == 1080)
            currentIndex = 1;

        resolutionDropdown.value = currentIndex;
        resolutionDropdown.RefreshShownValue();

        // 이벤트 등록
        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
    }

    void OnResolutionChanged(int index)
    {
        switch (index)
        {
            case 0:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            case 1:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;
        }

        Debug.Log("해상도 변경: " + Screen.width + "x" + Screen.height); //빌드해야 제대로 적용합니다
    }
}
