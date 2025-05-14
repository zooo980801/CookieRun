using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : Item
{
    public float value = 12f;
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

    public void ApplyEffect()
    {
        GameManager.Instance.AddScore((int)value);
    }

   

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[CoinItem] Trigger with: {other.gameObject.name}");
        if (other.CompareTag("Player"))
        {
            ApplyEffect();
            gameObject.SetActive(false);
        }
    }


}
