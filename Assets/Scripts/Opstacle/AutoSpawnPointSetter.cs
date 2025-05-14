using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutoSpawnPointSetter : MonoBehaviour
{
    [SerializeField] private float offsetX = 1f; // 카메라 우측에서 얼마나 떨어질지

    private void Start()
    {
        // 카메라 기준으로 우측 화면 가장자리 위치 계산
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("Main Camera를 찾을 수 없습니다.");
            return;
        }

        // Viewport 우측 상단(1, 0.5)의 월드 위치를 기준으로 배치
        Vector3 rightEdge = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, cam.nearClipPlane));
        transform.position = new Vector3(rightEdge.x + offsetX, 0, 0);
    }
}
