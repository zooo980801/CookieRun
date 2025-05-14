using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    private StartUIManager startUiManager;
    [Header("슬라이더")]
    public Slider allVolumeSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;
    [Header("퍼센트텍스트")]
    public TextMeshProUGUI allvolumePercent;
    public TextMeshProUGUI bgmPercent;
    public TextMeshProUGUI sfxPercent;
    public void Init(StartUIManager startUiManager)
    {
        this.startUiManager = startUiManager;
        exitButton.onClick.AddListener(OnClickExitButton);

        //올 볼륨
        allVolumeSlider.value = AudioListener.volume;
        UpdateAllVolumeText(AudioListener.volume);
        allVolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        //브금
        float bgmVol = 1.0f;                       // 기본 100 %
        bgmSlider.value = bgmVol;
        BGMManager.instance.SetVolume(bgmVol);
        UpdateBGMText(bgmVol);
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        //SFX
        float sfxVol = 1.0f;                       // 기본 100 %
        sfxSlider.value = sfxVol;
        SFXManager.Instance.SetVolume(sfxVol);
        UpdateSFXText(sfxVol);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

    }
    public void OnClickExitButton()
    {
        startUiManager.HideOptionUI();
    }
    void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        UpdateAllVolumeText(value);
    }

    void OnBGMVolumeChanged(float value)
    {
        BGMManager.instance.SetVolume(value);
        UpdateBGMText(value);
    }

    void OnSFXVolumeChanged(float value)
    {
        SFXManager.Instance.SetVolume(value);
        UpdateSFXText(value);
    }


    void UpdateAllVolumeText(float value)
    {
        int percent = Mathf.RoundToInt(value * 100);
        allvolumePercent.text = percent + "%";
    }
    void UpdateBGMText(float value)
    {
        int percent = Mathf.RoundToInt(value * 100);
        bgmPercent.text = percent + "%";
    }

    void UpdateSFXText(float value)
    {
        int percent = Mathf.RoundToInt(value * 100);
        sfxPercent.text = percent + "%";
    }
}
