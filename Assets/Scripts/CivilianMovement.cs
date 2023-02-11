using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;

    private float rightDir = 1.0f;

    void Start()
    {
    }

    void FixedUpdate()
    {
        if (rightDir == 1.0f) {
            transform.Translate(Vector2.right * moveSpeed * Time.fixedDeltaTime);
        }
        if (rightDir == -1.0f) {
            transform.Translate(Vector2.left * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rightDir = rightDir * (-1);
    }
}
