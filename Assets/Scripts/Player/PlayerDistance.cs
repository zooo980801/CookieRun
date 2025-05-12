using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistance : MonoBehaviour
{
    [SerializeField] private float startPosX;
    [SerializeField] public float distance = 0f;
    
    void Start()
    {
        startPosX = transform.position.x;
    }

    void Update()
    {
        distance = transform.position.x - startPosX;
    }
}
