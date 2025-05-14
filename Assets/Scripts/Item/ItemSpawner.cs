using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public ItemManager itemManager;
    public List<GroundData> groundTiles;
    public Camera mainCamera;

    private float spawnOffset = 1f;
    private float spawnInterval = 0.5f; // 아이템 생성 주기
    private float timeSinceLastSpawn = 0f;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // 카메라가 지정되지 않으면 메인 카메라를 자동으로 찾음
        }

        SpawnItemsOnGround();
    }

    // 타일에 아이템을 생성하는 함수
    void SpawnItemsOnGround()
    {
        foreach (var tile in groundTiles)
        {
            // 타일이 활성화되어 있고, 플레이어가 지나간 자리가 아니라면 아이템 생성
            if (tile.Skin == 0 && tile.IsActive && !IsTileInPlayerPassedArea(tile))
            {
                Vector3 spawnPosition = new Vector3(
                    tile.Position.x + Random.Range(-spawnOffset, spawnOffset), // 고정된 간격으로 x 좌표 조정
                    tile.Position.y + Random.Range(0.5f, 1.5f), // 아이템이 플레이어가 먹을 수 있는 y 위치에 생성
                    0
                );

                // 카메라 화면 안에 위치하는지 확인
                if (IsPositionInCameraView(spawnPosition))
                {
                    ItemType randomItem = (ItemType)Random.Range(0, 4);

                    switch (randomItem)
                    {
                        case ItemType.Coin:
                            var coin = itemManager.coinPool.GetObject();
                            coin.transform.position = spawnPosition;
                            break;
                        case ItemType.Heal:
                            var heal = itemManager.healPool.GetObject();
                            heal.transform.position = spawnPosition;
                            break;
                        case ItemType.SpeedUp:
                            var speedUp = itemManager.speedUpPool.GetObject();
                            speedUp.transform.position = spawnPosition;
                            break;
                        case ItemType.SpeedDown:
                            var speedDown = itemManager.speedDownPool.GetObject();
                            speedDown.transform.position = spawnPosition;
                            break;
                    }

                    Debug.Log($"아이템 생성 (타일 위치: {spawnPosition})");
                }
            }
        }
    }
    bool IsTileInPlayerPassedArea(GroundData tile)
    {
        float playerPositionX = itemManager.playerTransform.position.x;
        float tilePositionX = tile.Position.x;

        return tilePositionX < playerPositionX;
    }

    bool IsPositionInCameraView(Vector3 position)
    {
        // 카메라의 뷰포트 좌표로 변환
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(position);
        // 화면 안에 있으면 0~1 사이의 값이 됩니다.
        return viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1;
    }

}
