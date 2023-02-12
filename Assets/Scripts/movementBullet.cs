using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementBullet : MonoBehaviour
{
    public float speed = 15.0f;
    private GameObject Player;
    private Vector2 direction;

    void Start()
    {
        Player = GameObject.Find("Player");
        direction = (Player.transform.position - transform.position).normalized * speed;
        Destroy(gameObject, 5f);
    }

    void FixedUpdate() 
    {
        transform.Translate(direction * Time.fixedDeltaTime);
    }
}
