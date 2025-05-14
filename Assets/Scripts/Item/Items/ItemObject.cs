using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : Item, IItemEffect
{
    [SerializeField]
    private SpriteRenderer _renderer;
    public Sprite[] spriteImages;


    [SerializeField]
    private GroundObjectData _data
    {
        get { return data; }
        set { data = value; }
    }


    public override void Initialize(GroundObjectData data)
    {
        this._data = data;
        _renderer.sprite = spriteImages[data.type];
    }

    public void ApplyEffect(PlayerController player)
    {
        switch (_data.type) // 0코인, 1힐, 2스피드다운, 3스피드업
        {
            case 0:
                {
                    Debug.Log("코인 값:" + _data.value);
                    GameManager.Instance.AddScore(10);
                    break;
                }
            case 1:
                {
                    player.Heal(_data.value);
                    break;
                }
            case 2:
                {
                    player.SpeedChangeTemporary(_data.value, 2f);
                    break;
                }
            case 3:
                {
                    player.SpeedChangeTemporary(_data.value, 2f);
                    break;
                }
        }
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered: " + other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected!");
            var player = other.GetComponent<PlayerController>();
            SFXManager.Instance.CoinSFX();
            if (player != null)
            {
                ApplyEffect(player);
                gameObject.SetActive(false);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Trigger entered: " + other.transform.name);
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Player detected!");
            var player = other.transform.GetComponent<PlayerController>();
            if (player != null)
            {
                ApplyEffect(player);
                gameObject.SetActive(false);
            }
        }
    }

}
