using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
    public void SetRestart()
    {
        gameOverPanel.SetActive(true);
    }
}
