using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;

    private float rightDir = 1.0f;

    Blood_Manager bloodManager;

    private Animator animator;
    private Rigidbody2D rb;
    void Start()
    {
        bloodManager = GetComponent<Blood_Manager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("VelX", rb.velocity.x);
        animator.SetFloat("VelY", rb.velocity.y);
    }

    void FixedUpdate()
    {
        if (rightDir == 1.0f) {
            rb.velocity = Vector2.right * moveSpeed;
        }
        if (rightDir == -1.0f) {
            rb.velocity = Vector2.left * moveSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rightDir = rightDir * (-1);
        var p = collision.gameObject.GetComponent<Player_Movement>();
        if (p != null && p.isRunning)
        {
            bloodManager.DoBloodCollision(collision);
            p.IKilledSomeone();
        }
    }
}
