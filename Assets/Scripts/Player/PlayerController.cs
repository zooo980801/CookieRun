using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Unit
{
    private TutorialManager tutorialManager;

    private bool isGodMode = false;
    private float godModeTimer = 0f;
    private bool isDead = false;

    [SerializeField] private int maxJumpCount = 2; // 최대 점프 횟수
    private int currentJumpCount = 0;

    public float hitDamage = 20f; // 충돌 시 감소할 체력
    public float damageByTime = 5f; //시간에 따른 감소 체력
    private float limitPosY = -20f;
    private bool wasGroundedLastFrame = false;

    public float CurrentHp => Hp;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            GameManager.Instance.AddScore(10);
            SFXManager.Instance.CoinSFX();

            if (tutorialManager != null && tutorialManager.IsCurrentStep(TutorialStep.CollectCoin))
            {
                tutorialManager.AdvanceStep();
            }

            Destroy(other.gameObject);
            return;
        }
        if (isGodMode || isDead) return;

        if (other.CompareTag("Enemy") || other.CompareTag("Obstacle"))
        {
            SFXManager.Instance.HitSFX();
            TakeDamage(hitDamage);

            if (tutorialManager != null && tutorialManager.IsCurrentStep(TutorialStep.TakeDamage))
            {
                tutorialManager.AdvanceStep();
            }
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

    private void Fall()
    {
        if (transform.position.y < limitPosY)
        {
            Hp = 0f;
            isDead = true;
            GameManager.Instance.GameOver();
        }
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

        tutorialManager = FindObjectOfType<TutorialManager>();
    }

    private void Update()
    {
        DecreaseHpByTime();
        Fall();     //낙하 게임오버

        //땅에 오브젝트가 닿았는지 확인
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundRayLength, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * groundRayLength, Color.red);

        if (isGrounded && !wasGroundedLastFrame)
        {
            currentJumpCount = 0;
        }
        wasGroundedLastFrame = isGrounded;
        //점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 튜토리얼 단계 확인
                Jump();
                if (tutorialManager != null && tutorialManager.IsCurrentStep(TutorialStep.Jump))
                {
                    tutorialManager.AdvanceStep();
                }
            
        }

        animCtrl.JumpAnim(isGrounded);

        //슬라이드
        // 현재 LeftShift 키 상태 체크
        PressedShift = Input.GetKey(KeyCode.LeftShift) || GameUI.Instance?.IsSlidePressed() == true;
        Slide(PressedShift);

        if (PressedShift && tutorialManager != null && tutorialManager.IsCurrentStep(TutorialStep.Slide))
        {
            tutorialManager.AdvanceStep();
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
        if (currentJumpCount >= maxJumpCount)
        {
            Debug.Log("점프 불가: currentJumpCount = " + currentJumpCount);
            return;
        }

        SFXManager.Instance.JumpSFX();
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        currentJumpCount++;

        Debug.Log("점프! currentJumpCount = " + currentJumpCount);
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
    public override void DecreaseHpByTime()
    {
        //시간에 따른 체력 감소
        Hp -= damageByTime * Time.deltaTime;
        
    }
    public void Heal(float amount)
    {
        if (100f - Hp < amount)
        {
            Hp += amount - (100f - Hp);
        }
        else
        {
            Hp += amount;
        }

    }

    public void SpeedChange(float amount)
    {
        Speed += amount;
    }

    public void Damaged(float amount)
    {
        if (Hp > 0)
        {
            Hp -= amount;
        }
        else
        {
            Hp = 0;
        }

    }
}