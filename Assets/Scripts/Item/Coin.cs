using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int coinLayer = LayerMask.NameToLayer("Item");
        int playerLayer = LayerMask.NameToLayer("Player");

        if(collision.gameObject.layer == playerLayer)
        {
            GameManager.Instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
