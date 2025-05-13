using UnityEngine;

public class Obstarcle : Item
{  
    [SerializeField]
    private SpriteRenderer _renderer;
    
    [SerializeField]
    private GroundObjectData _data
    {
        get { return data; }
        set { data = value; }
    }

    void Start()
    {
        this._renderer = GetComponent<SpriteRenderer>();
    }
  

    public override void Initialize(GroundObjectData data)
    {
        this._data = data;
    }

}