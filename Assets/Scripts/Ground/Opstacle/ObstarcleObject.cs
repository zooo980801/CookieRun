using UnityEngine;

public class ObstarcleObject : Item
{
    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    private GroundObjectData _data
    {
        get { return data; }
        set { data = value; }
    }

    [SerializeField]
    private float originY = 1.0f;

    void Start()
    {
        this._renderer = GetComponent<SpriteRenderer>();
    }


    public override void Initialize(GroundObjectData data)
    {
        this._data = data;
        if (data.type == 0) // 일반 장애물
        {
            transform.localPosition = new Vector3(transform.position.x, originY, 0);
        }
        if (data.type == 1) // 공중 장애물
        {
            transform.localPosition = new Vector3(transform.position.x, originY + 1f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered: " + other.transform.name);
        if (other.CompareTag("Player"))
        {
            SFXManager.Instance.HitSFX();
            Debug.Log("Player detected!");
            var player = other.transform.GetComponent<PlayerController>();
            if (player != null)
            {
                ApplyEffect(player);
            }
        }
    }

    public void ApplyEffect(PlayerController player)
    {
        player.Damaged(_data.value);
    }

}