using System.Collections.Generic;
using UnityEngine;


public abstract class Ground : MonoBehaviour
{
    protected MapData data { get; set; }

    public abstract void Initialize(float groundPosX, MapData data);

}