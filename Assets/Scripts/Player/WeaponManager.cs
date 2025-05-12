using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> weaponList = new List<Sprite>();
    private Dictionary<string, Sprite> weaponDict = new Dictionary<string, Sprite>();
    
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer == null) { Debug.LogError("SpriteRenderer가 없습니다."); }
    }

    void Start()
    {
        //필수과제 완료 후 진행
        foreach (var item in weaponList)
        {
            weaponDict[item.name] = item;
        }
    }

    void SetWeapon(string itemName)
    {
        spriteRenderer.sprite = weaponDict[itemName];
    }
}
