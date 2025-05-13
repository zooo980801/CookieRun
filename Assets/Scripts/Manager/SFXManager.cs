using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SFXManager : MonoBehaviour
{
    public AudioClip coinClip;
    public AudioClip hitClip;
    public AudioClip jumpClip;

    private AudioSource audioSource;

    public static SFXManager Instance;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) { Debug.LogError("AudioSource가 없음.");}

        if (Instance != null)
        {
            Destroy(gameObject); // 중복 방지
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void CoinSFX()
    {
        audioSource.PlayOneShot(coinClip);
    }

    public void HitSFX()
    {
        audioSource.PlayOneShot(hitClip);
    }

    public void JumpSFX()
    {
        audioSource.PlayOneShot(jumpClip);
    }
}
