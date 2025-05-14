using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpItem : Item, IItemEffect
{
    public float speedUp = 1.5f;
    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    private GroundObjectData _data
    {
        get { return data; }
        set { data = value; }
    }

    public override void Initialize(GroundObjectData data)
    {
        this._data = data;
    }

    public void ApplyEffect(PlayerController player)
    {
     
        player.SpeedChange(speedUp); 

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                ApplyEffect(player);
                gameObject.SetActive(false);
            }
        }
    }
}
