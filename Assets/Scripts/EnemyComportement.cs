using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComportement : MonoBehaviour
{
    public GameObject Player;
    public float range = 100.0f;
    public float speed = 6.0f;
    public float moveSpeed = 3.0f;
    private float rightDir = 1.0f;

    private float distance;
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, Player.transform.position);
        if (distance < range) {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), Player.transform.position, speed * Time.fixedDeltaTime);
        } else {
            if (rightDir == 1.0f) {
            transform.Translate(Vector2.right * moveSpeed * Time.fixedDeltaTime);
            }
            if (rightDir == -1.0f) {
                transform.Translate(Vector2.left * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rightDir = rightDir * (-1);
    }
}
