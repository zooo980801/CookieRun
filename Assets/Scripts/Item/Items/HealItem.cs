using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : Item, IItemEffect
{
    public float value = 15f;
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
         player.Heal(value);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                ApplyEffect(player);
                Destroy(gameObject);
            }
        }
    }
}
