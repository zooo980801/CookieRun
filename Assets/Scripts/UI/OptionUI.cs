using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    private StartUIManager startUiManager;
    public Slider volumeSlider;
    public TextMeshProUGUI volumePercent;
    public void Init(StartUIManager startUiManager)
    {
        this.startUiManager = startUiManager;
        exitButton.onClick.AddListener(OnClickExitButton);
        volumeSlider.value = AudioListener.volume;
        UpdateVolumeText(AudioListener.volume);

        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }
    public void OnClickExitButton()
    {
        startUiManager.HideOptionUI();
    }
    void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        UpdateVolumeText(value);
    }

    void UpdateVolumeText(float value)
    {
        int percent = Mathf.RoundToInt(value * 100);
        volumePercent.text = percent + "%";
    }
}
