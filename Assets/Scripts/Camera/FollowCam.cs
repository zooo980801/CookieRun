using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float camHeight = 0f;
    
    private Vector3 playerPos;
    private Vector3 camOffset;

    private void Awake()
    {
        if (playerTransform == null)
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (playerTransform == null)
            Debug.LogError("playerTransform을 찾을 수 없습니다.");

        camOffset = new Vector3(5f, 0f, -10f);
    }
    
    void Update()
    {
        playerPos = new Vector3(playerTransform.position.x, camHeight, 0f);
        transform.position = playerPos + camOffset;
    }
}
