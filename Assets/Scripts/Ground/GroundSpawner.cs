using System;
using UnityEngine;


public class GroundSpawner : MonoBehaviour
{
    [SerializeField]
    private GroundManager groundManager;

    [SerializeField]
    private Transform playerTransform;
    private float offset;


    private void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        offset = playerTransform.position.x - transform.position.x;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.CompareTag("Ground"))
        {
            groundManager.DeSpawn(other.transform.parent.gameObject);
            Debug.Log("스포너가 그라운드에 닿음");
        }
        if (other.transform.CompareTag("StartGround"))
        {
            Destroy(other.gameObject);
        }
    }
    private void Update()
    {
        if(playerTransform != null)
        {
            transform.position = new Vector3(playerTransform.position.x - offset, transform.position.y, transform.position.z);
        }
    }

}