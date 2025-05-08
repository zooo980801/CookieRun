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

    [SerializeField] private bool isGrounded = false;
    private float groundRayLength = 1.2f;
    [SerializeField] private LayerMask groundLayer;
    
    protected Rigidbody2D rb;
    [SerializeField] protected Animator animator;
    protected BoxCollider2D collider;

    private Vector2 currentColliderSize;
    private Vector2 currentColliderOffset;
    private Vector2 colliderSize = new Vector2(1, 0.6f);
    private Vector2 colliderOffset = new Vector2(0.4f, -0.6f);

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) { Debug.LogError("Rigidbody2D가 없습니다.");}

        animator = GetComponentInChildren<Animator>();
        if (animator == null) { Debug.LogError("Animator가 없습니다.");}

        collider = GetComponent<BoxCollider2D>();
        if (collider == null) { Debug.LogError("BoxCollider2D가 없습니다."); }
        
        currentColliderSize = collider.size;
        currentColliderOffset = collider.offset;
    }

    protected void Jump()
    {
        if (rb != null)
        {
            //땅에 오브젝트가 닿았는지
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundRayLength, groundLayer);
            
            //땅에 닿았을 때만 점프
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    protected void Slide()
    {
        bool pressedControl = Input.GetKey(KeyCode.LeftControl);
        
        if (isGrounded)
        {
            if (pressedControl)
            {
                animator.SetBool("IsSlide", true);
                collider.size = colliderSize;
                collider.offset = colliderOffset;
            }
            else
            {
                animator.SetBool("IsSlide", false);
                collider.size = currentColliderSize;
                collider.offset = currentColliderOffset;
            }
        }
    }
}
