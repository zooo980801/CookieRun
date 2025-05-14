using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroundManager : MonoBehaviour
{
    [SerializeField]
    private MapDataWrap _mapData;
    [SerializeField]
    private GameObject groundPrefab;

    private ObjectPool<GroundObject> groundPool;
    public int initialGroundCount = 10;

    public event EventHandler SpawnEvent;
    public event Action<GameObject> DeSpawnEvent;

    [Header("맵 생성 포인트")]
    [SerializeField]
    private float groundPosX;
    [SerializeField]
    private float groundXDistance;
    [SerializeField]
    private int spawnCount;

    //추가된 내용
    private List<GroundObject> activeGrounds = new List<GroundObject>();

    private void Start()
    {
        groundPool = new ObjectPool<GroundObject>(groundPrefab, initialGroundCount);
        SpawnEvent += SpawnTile;
        DeSpawnEvent += DeSpawnTile;
        spawnCount = 0;
        Initialize();
    }

    public void Initialize()
    {
        for(int i=0; i < initialGroundCount-1 ; i++)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        SpawnEvent?.Invoke(this, EventArgs.Empty);
    }

    public void DeSpawn(GameObject obj)
    {
        DeSpawnEvent?.Invoke(obj);
    }

    public void SpawnTile(object sendder, EventArgs e)
    {
        GroundObject @Go = groundPool.GetObject();

        System.Random random = new System.Random();
        @Go.Initialize(_mapData.MapData[random.Next(_mapData.MapData.Count)]); //난이도 조절 시 x=>x.Difficult
        @Go.transform.position = new Vector3(groundPosX, -2.56f, 0);

        groundPosX += groundXDistance;

        if (!activeGrounds.Contains(@Go))
            activeGrounds.Add(@Go);
    }

    public void DeSpawnTile(GameObject obj)
    {
        Debug.Log("디스폰");
        var @Go = obj.GetComponent<GroundObject>();
        if (activeGrounds.Contains(@Go))
            activeGrounds.Remove(@Go);

        groundPool.ReturnObject(@Go);
        Spawn();
    }

    //// 클래스 json으로 바로 꺼내기
    [ContextMenu("데이터 json 저장")]
    void SaveDataToJson()
    {
        string @json = JsonUtility.ToJson(_mapData);
        string path =  Path.Combine(Application.dataPath,"mapData.json");

        File.WriteAllText(path, @json);
        Debug.Log(path);
    }

    //추가된 내용
    public List<GroundObject> GetActiveGrounds()
    {
        return activeGrounds;
    }
}