using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Unit
{
    
    protected override float Hp
    {
        get { return hp; }
        set
        {
            hp = Mathf.Clamp(value, 0, fullHP);
        }
    }
    
    protected override float JumpForce
    {
        get { return jumpForce; }
        set
        {
            jumpForce = Mathf.Max(0, value);
        }
    }

    protected override float Speed
    {
        get { return speed; }
        set
        {
            speed = Mathf.Max(0, value);
        }
    }

    private void Awake()
    {
        base.Awake();
        Hp = 100f;
        JumpForce = 5f;
        Speed = 5f;
    }
    
    private void Update()
    {
        DecreaseHpByDistance();
        
        //땅에 오브젝트가 닿았는지 확인
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundRayLength, groundLayer);
        
        //점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        animCtrl.JumpAnim(isGrounded);
        
        //슬라이드
        PressedShift = Input.GetKey(KeyCode.LeftShift);
        if (PressedShift)
        {
            Slide(PressedShift);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        tr.Translate(Vector3.right * Speed * Time.fixedDeltaTime, Space.World);
    }
    
    public override void Jump()
    {
        rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    public override void Slide(bool PressedShift)
    {
        animCtrl.SlideAnim(PressedShift);
    }

    public override void DecreaseHpByDistance()
    {
        //거리에 따른 체력 감소
        hp -= playerDistance.distance * 0.001f;
    }
}