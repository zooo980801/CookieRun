using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Coin = 0,
    Heal = 1,
    SpeedUp = 2,
    SpeedDown = 3
}

public class ItemManager : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject healItemPrefab;
    public GameObject speedUpPrefab;
    public GameObject speedDownPrefab;

    public ObjectPool<CoinItem> coinPool;
    public ObjectPool<HealItem> healPool;
    public ObjectPool<SpeedUpItem> speedUpPool;
    public ObjectPool<SpeedDownItem> speedDownPool;

    public float spawnInterval = 5f;  // 주기적인 아이템 생성 주기
    private float timeSinceLastSpawn = 0f;

    void Awake()
    {
        coinPool = new ObjectPool<CoinItem>(coinPrefab, 10);
        healPool = new ObjectPool<HealItem>(healItemPrefab, 5);
        speedUpPool = new ObjectPool<SpeedUpItem>(speedUpPrefab, 5);
        speedDownPool = new ObjectPool<SpeedDownItem>(speedDownPrefab, 5);
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            // 주기적으로 아이템을 생성
            SpawnRandomItem();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnRandomItem()
    {
        // 랜덤 아이템 타입을 선택
        ItemType randomItem = (ItemType)Random.Range(0, 4); // 0~3 (Coin~SpeedDown)

        // 랜덤 위치 선택 (이 부분은 위치를 나중에 지정할 수 있도록 수정)
        Vector3 spawnPosition = new Vector3(
            Random.Range(-10f, 10f), // 예시: X 범위
            0f,                      // Y 값 고정
            Random.Range(-10f, 10f)  // 예시: Z 범위
        );

        // 아이템을 풀에서 꺼내서 배치
        switch (randomItem)
        {
            case ItemType.Coin:
                var coin = coinPool.GetObject();
                coin.transform.position = spawnPosition;
                break;
            case ItemType.Heal:
                var heal = healPool.GetObject();
                heal.transform.position = spawnPosition;
                break;
            case ItemType.SpeedUp:
                var speedUp = speedUpPool.GetObject();
                speedUp.transform.position = spawnPosition;
                break;
            case ItemType.SpeedDown:
                var speedDown = speedDownPool.GetObject();
                speedDown.transform.position = spawnPosition;
                break;
        }

        Debug.Log("아이템 생성: " + randomItem.ToString() + " 위치: " + spawnPosition);
    }
}
