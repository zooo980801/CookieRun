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

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public override void Initialize(GroundObjectData data)
    {
        _renderer.sprite = spriteImages[data.skin];

        transform.localPosition = new Vector3(transform.localPosition.x, data.PositionY + 1, 0);
    }

    public void ApplyEffect(PlayerController player)
    {
        switch (_data.type) // 0코인, 1힐, 2스피드다운, 3스피드업
        {
            case 0:
                {
                    GameManager.Instance.AddScore((int)_data.value);
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
