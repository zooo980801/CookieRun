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
    public GameObject SpeedUpPrefab;
    public GameObject SpeedDownPrefab;

    public TextAsset jsonFile;

    private ObjectPool<CoinItem> coinPool;
    private ObjectPool<HealItem> healPool;
    private ObjectPool<SpeedUpItem> speedUpPool;
    private ObjectPool<SpeedDownItem> speedDownPool;

    private void Awake()
    {
        coinPool = new ObjectPool<CoinItem>(coinPrefab, 10);
        healPool = new ObjectPool<HealItem>(healItemPrefab, 5);
        speedUpPool = new ObjectPool<SpeedUpItem>(SpeedUpPrefab, 5);
        speedDownPool = new ObjectPool<SpeedDownItem>(SpeedDownPrefab, 5);
    }

    private void Start()
    {
        string json = jsonFile.text;
        MapDataWrap mapDataWrap = JsonUtility.FromJson<MapDataWrap>(json);

        foreach (var mapData in mapDataWrap.MapData)
        {
            SpawnItems(mapData.GroundObjectData);
        }
    }

    private void SpawnItems(List<GroundObjectData> items)
    {
        foreach (var item in items)
        {
            Vector3 position = new Vector3(item.Position.X, 0, 0);
            switch ((ItemType)item.type)
            {
                case ItemType.Coin:
                    var coin = coinPool.GetObject();
                    coin.transform.position = position;
                    coin.Initialize(item);
                    break;
                case ItemType.Heal:
                    var heal = healPool.GetObject();
                    heal.transform.position = position;
                    heal.Initialize(item);
                    break;
                case ItemType.SpeedUp:
                    var speedUp = speedUpPool.GetObject();
                    speedUp.transform.position = position;
                    speedUp.Initialize(item);
                    break;
                case ItemType.SpeedDown:
                    var speedDown = speedDownPool.GetObject();
                    speedDown.transform.position = position;
                    speedDown.Initialize(item);
                    break;
            }
        }
    }
}