using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Unit
{
    void Start()
    {
        //체력과 속도 지정
        Hp = 100f;
        JumpForce = 5f;
    }

    private void Update()
    {
        Jump();
        Slide();
    }
}