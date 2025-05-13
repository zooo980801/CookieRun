using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public ItemManager itemManager;  // 아이템 매니저를 참조
    public List<GroundData> groundTiles;  // 아이템을 생성할 타일 리스트

    void Start()
    {
        SpawnItemsOnGround();
    }

    // 타일에 아이템을 생성하는 함수
    void SpawnItemsOnGround()
    {
        foreach (var tile in groundTiles)
        {
            // 조건에 맞는 타일에서 아이템을 생성
            if (tile.Skin == 0 && tile.IsActive)  // 예시: Skin == 0인 타일에서만 아이템 생성
            {
                Vector3 spawnPosition = tile.Position;

                // 아이템 생성 (아이템 타입은 임의로 선택하거나 설정된 타입을 사용할 수 있음)
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
