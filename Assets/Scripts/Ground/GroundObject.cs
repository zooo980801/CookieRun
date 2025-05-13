using System.Collections.Generic;
using UnityEngine;


public class GroundObject : Ground
{
    public List<Transform> groundPrefabs;
    public List<Transform> itemPrefabs;

    public MapData _data
    {
        get { return data; }
        set { data = value; }
    }
    public override void Initialize(MapData data)
    {
        this._data = data;
    }

   /* public override void InitItems()
    {

    }*/
}
