using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public ItemManager itemManager;  
    public List<GroundData> groundTiles;  

    void Start()
    {
        SpawnItemsOnGround();
    }

    // 타일에 아이템을 생성하는 함수
    void SpawnItemsOnGround()
    {
        foreach (var tile in groundTiles)
        {
           
            if (tile.Skin == 0 && tile.IsActive)  
            {
                Vector3 spawnPosition = tile.Position;

                
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
