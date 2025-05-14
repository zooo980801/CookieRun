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

    public Transform playerTransform;

    public float spawnInterval = 5f;  // 주기적인 아이템 생성 주기
    private float timeSinceLastSpawn = 0f;

    void Awake()
    {
        coinPool = new ObjectPool<CoinItem>(coinPrefab, 10);
        healPool = new ObjectPool<HealItem>(healItemPrefab, 5);
        speedUpPool = new ObjectPool<SpeedUpItem>(speedUpPrefab, 5);
        speedDownPool = new ObjectPool<SpeedDownItem>(speedDownPrefab, 5);
    }

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
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
        if (groundManager == null)
        {
            Debug.LogError("groundManager가 null입니다. 아이템을 생성할 수 없습니다.");
            return;
        }

        var activeGrounds = groundManager.GetActiveGrounds();
        if (activeGrounds == null || activeGrounds.Count == 0)
        {
                Debug.LogError("활성화된 Ground가 없습니다.");
                return;
        }

        Vector3 playerPosition = playerTransform.position;

        float interval = 1f;
        float xPos = Mathf.FloorToInt(activeGrounds[Random.Range(0, activeGrounds.Count)].transform.position.x / interval) * interval;
        float yPos = playerPosition.y + 1f + Random.Range(-1f, 0.5f);

        Vector3 spawnPos = new Vector3(xPos, yPos, 0);

        if (Vector3.Distance(playerPosition, spawnPos) < 1.5f)
        {
            return;
        }

        Camera cam = Camera.main;
        Vector3 screenPos = cam.WorldToScreenPoint(spawnPos);
        if (screenPos.x >= 0 && screenPos.x <= Screen.width && screenPos.y >= 0 && screenPos.y <= Screen.height)
        {
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
}
