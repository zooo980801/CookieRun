using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D collider;

    private Vector2 currentColliderSize;
    private Vector2 currentColliderOffset;
    private Vector2 colliderSize = new Vector2(1, 0.6f);
    private Vector2 colliderOffset = new Vector2(0.4f, -0.6f);

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator == null) { Debug.LogError("Animator가 없습니다."); }
        
        collider = GetComponent<BoxCollider2D>();
        if (collider == null) { Debug.LogError("BoxCollider2D가 없습니다."); }
        
        currentColliderSize = collider.size;
        currentColliderOffset = collider.offset;
    }

    public void SlideAnim(bool PressedShift)
    {
        if (PressedShift)
        {
            ColliderCtrl(colliderSize, colliderOffset);
        }
        else
        {
            ColliderCtrl(currentColliderSize, currentColliderOffset);
        }
        animator.SetBool("IsSlide", PressedShift);
    }

    public void JumpAnim(bool isGrounded)
    {
        animator.SetBool("IsGrounded", isGrounded);
    }

    private void ColliderCtrl(Vector2 _size, Vector2 _offset)
    {
        collider.size = _size;
        collider.offset = _offset;
    }
}
