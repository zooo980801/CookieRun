using System.Collections.Generic;
using UnityEngine;


public class GroundObject : Ground
{
    public List<Transform> groundPrefabs;
    public List<Transform> obstarcleprefabs;
    public List<Transform> itemPrefabs;

    public MapData _data
    {
        get { return data; }
        set { data = value; }
    }

    public override void Initialize(MapData data)
    {
        Debug.Log("땅 이니셜라이즈");
        this._data = data;
        for (int i = 0; i < groundPrefabs.Count; i++)
        {
            obstarcleprefabs[i].gameObject.SetActive(false);
            itemPrefabs[i].gameObject.SetActive(false);
        }

        InitObstarcles();
        InitItems();
    }

    public void InitObstarcles()
    {
        Debug.Log("땅 이니셜라이즈");
        if (_data.GroundObjectData.Count > 0 && _data.GroundObjectData != null)
        {
            for (var i = 0; i < _data.GroundObjectData.Count; i++)
            {
                if (_data.GroundObjectData[i].ObjectType == 0) // 0장애물
                {
                    obstarcleprefabs[_data.GroundObjectData[i].Position].gameObject.SetActive(true);
                    obstarcleprefabs[_data.GroundObjectData[i].Position].GetComponent<Item>().Initialize(_data.GroundObjectData[i]);
                }
            }
        }
    }

    public void InitItems()
    {
        if (_data.GroundObjectData.Count > 0 && _data.GroundObjectData != null)
        {
            for (var i = 0; i < _data.GroundObjectData.Count; i++)
            {
                if (_data.GroundObjectData[i].ObjectType == 1) // 1아이템
                {
                    itemPrefabs[_data.GroundObjectData[i].Position].gameObject.SetActive(true);
                    itemPrefabs[_data.GroundObjectData[i].Position].GetComponent<Item>().Initialize(_data.GroundObjectData[i]);
                }
            }
        }
    }

}
