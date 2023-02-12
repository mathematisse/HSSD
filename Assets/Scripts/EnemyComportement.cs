using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComportement : MonoBehaviour
{
    public GameObject Player;
    public GameObject Bullet;
    public Transform firePoint;

    public float range = 15.0f;
    public float attackSpeed = 6.0f;
    public float moveSpeed = 3.0f;
    public float fireRate = 1.0f;
    private float rightDir = 1.0f;
    private float nextFire = 0.0f;

    private float distance;
    Blood_Manager bloodManager;
    private Animator animator;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bloodManager = GetComponent<Blood_Manager>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("VelX", rb.velocity.x);
        animator.SetFloat("VelY", rb.velocity.y);
    }

    void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, Player.transform.position);
        if (distance < range) {
            var direction = (Player.transform.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * attackSpeed, direction.y * attackSpeed);
            Shoot();
        } else {
            if (rightDir == 1.0f) {
                rb.velocity = Vector2.right * moveSpeed;
            }
            if (rightDir == -1.0f) {
                rb.velocity = Vector2.left * moveSpeed;
            }
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

    void Shoot()
    {
        if(Time.fixedTime > nextFire) {
            nextFire = Time.fixedTime + fireRate;
            Instantiate(Bullet, firePoint.position, firePoint.rotation);
            soundManager.instance.PlayManagerClip(4);
        }
    }
}
