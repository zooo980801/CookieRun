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
    
    private void Awake()
    {
        base.Awake();
        Hp = 100f;
        jumpForce = 5f;
    }
    
    private void Update()
    {
        //땅에 오브젝트가 닿았는지
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundRayLength, groundLayer);
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        animCtrl.JumpAnim(isGrounded);
        
        bool PressedShift = Input.GetKey(KeyCode.LeftShift);
        Slide(PressedShift);
    }
    
    public override void Jump()
    {
        rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    public override void Slide(bool PressedShift)
    {
        animCtrl.SlideAnim(PressedShift);
    }
}