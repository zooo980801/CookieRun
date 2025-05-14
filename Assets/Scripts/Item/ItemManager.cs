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

    [SerializeField] private GroundManager groundManager;

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

        var activeGrounds = groundManager.GetActiveGrounds();
        if (activeGrounds == null || activeGrounds.Count == 0) return;

        var randomGround = activeGrounds[Random.Range(0, activeGrounds.Count)];
        Vector3 spawnPos = new Vector3(
            randomGround.transform.position.x + Random.Range(-1f, 1f),
            randomGround.transform.position.y + 0.5f,
            0
        );

        ItemType randomItem = (ItemType)Random.Range(0, 4);

        // 아이템을 풀에서 꺼내서 배치
        switch (randomItem)
        {
            case ItemType.Coin:
                var coin = coinPool.GetObject();
                coin.transform.position = spawnPos;
                break;
            case ItemType.Heal:
                var heal = healPool.GetObject();
                heal.transform.position = spawnPos;
                break;
            case ItemType.SpeedUp:
                var speedUp = speedUpPool.GetObject();
                speedUp.transform.position = spawnPos;
                break;
            case ItemType.SpeedDown:
                var speedDown = speedDownPool.GetObject();
                speedDown.transform.position = spawnPos;
                break;
        }

        Debug.Log("아이템 생성: " + randomItem.ToString() + " 위치: " + spawnPos);
    }
}
