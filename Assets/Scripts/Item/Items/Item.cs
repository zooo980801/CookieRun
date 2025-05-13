using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected GroundObjectData data;

    public abstract void Initialize(GroundObjectData data);
}