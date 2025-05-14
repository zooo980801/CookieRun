using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    //최대 체력
    protected const float fullHP = 100f;
    
    //HP
    [SerializeField] protected float hp;
    protected abstract float Hp { get; set; }
    
    //점프 파워
    [SerializeField] protected float jumpForce;
    protected abstract float JumpForce { get; set; }

    [SerializeField] protected float speed;
    public abstract float Speed { get; set; }
    
    //지면에 있는지, 지면과의 거리, 지면 레이어마스크
    [SerializeField] public bool isGrounded = false;
    [SerializeField] public bool PressedShift = false;
    [SerializeField] protected float groundRayLength = 1.2f;
    [SerializeField] protected LayerMask groundLayer;
    
    protected Rigidbody2D rb;
    protected Transform tr;

    protected PlayerAnimController animCtrl;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) { Debug.LogError("Rigidbody2D가 없습니다.");}

        animCtrl = GetComponent<PlayerAnimController>();
        if (animCtrl == null) { Debug.LogError("PlayerAnimController가 없습니다."); }

        tr = GetComponent<Transform>();
    }

    public abstract void Move();
    
    public abstract void Jump();

    public abstract void Slide(bool PressedShift);

    public abstract void DecreaseHpByTime();
}
