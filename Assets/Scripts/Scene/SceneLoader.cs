using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static string nextScene;
    public static string transitionReason;
    public static void LoadScene(string sceneName, string reason = "")
    {
        Debug.Log("Loading to " + sceneName);
        nextScene = sceneName;
        transitionReason = reason;
        SceneManager.LoadScene("LoadingScene");
    }
}
