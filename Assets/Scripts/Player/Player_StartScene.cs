using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StartScene: MonoBehaviour
{
    private Animator animator;
    public float runDistance = 6f;      // Run 이동 거리
    public float slideDistance = 4f;    // Slide 이동 거리
    public float runSpeed = 2f;         // Run 속도
    public float slideSpeed = 3f;       // Slide 속도

    private bool isRunning = false;
    private bool isSliding = false;
    private bool alreadySecondRun = false;


    private Player_StartSceneSpawner spawner;
    private Vector3 targetPosition;
    public void Init(Player_StartSceneSpawner spawnerRef)
    {
        spawner = spawnerRef;
    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(RunAndSlideSequence());
    }

    void Update()
    {
        if (isRunning || isSliding)
        {
            float currentSpeed = isRunning ? runSpeed : slideSpeed;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) <= 0.01f)
            {
                if (isRunning && !isSliding && !alreadySecondRun)  // Run 완료 → Slide
                {
                    isRunning = false;
                    StartCoroutine(StartSlide());
                }
                else if (isSliding)
                {
                    isSliding = false;
                    StartCoroutine(StartSecondRun());
                }
                else if (isRunning && isSliding == false && alreadySecondRun) // SecondRun 완료 → Finish
                {
                    FinishSequence();
                }
            }
        }
    }

    private System.Collections.IEnumerator RunAndSlideSequence()
    {
        isRunning = true;
        animator.SetBool("IsRunning", true);
        targetPosition = transform.position + Vector3.right * runDistance;
        yield return null;
    }

    private System.Collections.IEnumerator StartSlide()
    {
        animator.SetBool("IsRunning", false);
        animator.SetTrigger("Slide");
        isSliding = true;
        targetPosition = transform.position + Vector3.right * slideDistance;
        yield return null;
    }

    private System.Collections.IEnumerator StartSecondRun()
    {
        animator.SetBool("IsRunning", true);
        isRunning = true;
        alreadySecondRun = true;  // SecondRun 시작할 때 플래그 ON
        targetPosition = transform.position + Vector3.right * (runDistance + 1f);
        yield return null;
    }
    private void FinishSequence()
    {
        animator.SetBool("IsRunning", false);
        isRunning = false;
        spawner.SpawnNextPlayer();
        Destroy(gameObject);
    }
}
