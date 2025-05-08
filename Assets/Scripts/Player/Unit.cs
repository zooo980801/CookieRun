using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //최대 체력
    private const float fullHP = 100f;
    
    //HP
    [SerializeField] private float hp;
    protected float Hp
    {
        get { return hp; }
        set
        {
            hp = Mathf.Clamp(value, 0, fullHP);
        }
    }

    [SerializeField] private float jumpForce;
    protected float JumpForce
    {
        get { return jumpForce; }
        set
        {
            jumpForce = Mathf.Max(0, value);
        }
    }
    
    
    
    protected Rigidbody2D rb;
    [SerializeField] protected Animator animator;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) { Debug.LogError("Rigidbody2D가 없습니다.");}

        animator = GetComponentInChildren<Animator>();
        if (animator == null) { Debug.LogError("Animator가 없습니다.");}
    }

    protected void Jump()
    {
        if (rb != null)
        {
            //땅에 닿았을 때만 점프되게 할 예정
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    protected void Slide()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetBool("IsSlide", true);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetBool("IsSlide", false);
        }
        
    }
}
