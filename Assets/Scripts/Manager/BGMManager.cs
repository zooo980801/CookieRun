using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public AudioClip startClip;
    public AudioClip mainClip;

    private AudioSource audioSource;

    public static BGMManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // 중복 방지
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "StartScene":
                Play(startClip);
                break;

            case "SampleScene":
                Play(mainClip);
                break;

            case "LoadingScene":
                Stop();
                break;

            default:
                Stop(); // 그 외 씬에서는 BGM 없음
                break;
        }
    }

    void Play(AudioClip clip)
    {
        if (audioSource.clip == clip) return;

        audioSource.clip = clip;
        audioSource.Play();
    }

    void Stop()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }
}
