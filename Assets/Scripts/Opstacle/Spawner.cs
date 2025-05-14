using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("스폰 프리팹")]
    public GameObject coinPrefab;
    public GameObject wallPrefab;
    public GameObject potionPrefab;

    [Header("스폰 간격")]
    public float spawnInterval = 2f;

    [Header("X축 오프셋")]
    public float offsetX = 1f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            Spawn();
            timer = 0f;
        }
    }

    private void Spawn()
    {
        if (coinPrefab == null || wallPrefab == null || potionPrefab == null)
        {
            Debug.LogWarning("Spawner에 프리팹이 비어 있습니다!");
            return;
        }

        Camera cam = Camera.main;
        if (cam == null) return;

        Vector3 rightEdge = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, cam.nearClipPlane));
        float spawnX = rightEdge.x + offsetX;

        // 0: coin, 1: wall, 2: potion
        int rand = Random.Range(0, 3);
        GameObject prefabToSpawn;
        float spawnY;

        if (rand == 0)
        {
            prefabToSpawn = coinPrefab;
            spawnY = Random.Range(-1f, 1.5f);
        }
        else if (rand == 1)
        {
            prefabToSpawn = wallPrefab;
            spawnY = -1f;
        }
        else
        {
            prefabToSpawn = potionPrefab;
            spawnY = Random.Range(-0.5f, 1f); // 포션은 낮은 위치에
        }

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0);
        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }


}
