using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    public float moveSpeed = 5.0f;
    public float runSpeed = 15.0f;
    public float runTimeScale = 0.5f;

    private Rigidbody2D rb;
    private Vector2 direction;

    private bool isRunning;

    private Time_Manager timeManager;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        timeManager = FindObjectOfType<Time_Manager>();
    }

    void Update()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRunning = true;
            timeManager.SetTimeScale(runTimeScale);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isRunning = false;
            timeManager.SetTimeScale(1.0f);
        }
        animator.SetFloat("VelX", direction.x);
        animator.SetFloat("VelY", direction.y);
    }

    private void FixedUpdate()
    {
        if (isRunning)
        {
            rb.velocity = direction * runSpeed;
        }
        else
        {
            rb.velocity = direction * moveSpeed;
        }
    }
}
