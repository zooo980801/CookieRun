using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StartSceneSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;

    void Start()
    {
        SpawnNextPlayer();
    }

    public void SpawnNextPlayer()
    {
        GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        Player_StartScene playerScript = newPlayer.GetComponent<Player_StartScene>();
        playerScript.Init(this);
    }
}
