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
    }

    void FixedUpdate() 
    {
        transform.Translate(direction * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<Player_Movement>();
        if (player != null) player.IGotShot();
        Destroy(gameObject);
    }
}
