using System.Collections.Generic;
using System.Numerics;

[System.Serializable]
public class MapDataWrap
{
    public List<MapData> MapData;
}

[System.Serializable]
public class MapData
{
    public int Difficulty;
    public List<GroundData> GroundData;
    public List<GroundObjectData> GroundObjectData;
}

[System.Serializable]
public class GroundData
{
    public bool IsActive;
}

[System.Serializable]
public class GroundObjectData
{
    public int skin;
    public float value;
    public int Position;
    public int type;
    public int ObjectType;
}