using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Unit
{
    private bool isGodMode = false;
    private float godModeTimer = 0f;
    private bool isDead = false;

    public float hitDamage = 20f; // 충돌 시 감소할 체력

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            GameManager.Instance.AddScore(10);
            Destroy(other.gameObject);
            return;
        }
        if (isGodMode || isDead) return;

        if (other.CompareTag("Enemy") || other.CompareTag("Obstacle"))
        {
            TakeDamage(hitDamage);
        }
    }

    private void TakeDamage(float amount)
    {
        Hp -= amount;
        if (Hp <= 0)
        {
            isDead = true;
            GameManager.Instance.GameOver();
            return;
        }

        ActivateGodMode(GameManager.Instance.playerGodMode);
    }

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

    public override float Speed
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
        // 땅 체크
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundRayLength, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        animCtrl.JumpAnim(isGrounded);

        PressedShift = Input.GetKey(KeyCode.LeftShift);
        if (PressedShift)
        {
            Slide(PressedShift);
        }

        // 무적 시간 감소
        if (isGodMode)
        {
            godModeTimer -= Time.deltaTime;
            if (godModeTimer <= 0f)
            {
                isGodMode = false;
                GetComponentInChildren<SpriteRenderer>().color = Color.white; // 색 복원
            }
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

    public void ActivateGodMode(float duration)
    {
        isGodMode = true;
        godModeTimer = duration;
        StartCoroutine(FlashWhileInvincible());
    }

    private IEnumerator FlashWhileInvincible()
    {
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        while (isGodMode)
        {
            sr.color = new Color(1, 1, 1, 0.5f); // 반투명
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }
}