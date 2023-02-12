using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Dog_Manager : MonoBehaviour
{
    public Color start_Spawn_Color;
    public float spawn_duration = 2f;
    public AnimationCurve lightIntensity;
    private float spawn_timer = 0f;
    private Light2D spawn_light;
    private SpriteRenderer Dog_Sprite;
    private SpriteRenderer spawn_Line;
    private bool hasSpawned = false;
    public Transform player;
    private Rigidbody2D rb;
    private float speed = 10f;
    private Vector2 direct;
    
    // Start is called before the first frame update
    void Start()
    {
        spawn_light = GetComponentsInChildren<Light2D>()[0];
        var sr = GetComponentsInChildren<SpriteRenderer>();
        Dog_Sprite = sr[0];
        spawn_Line = sr[1];
        Dog_Sprite.color = start_Spawn_Color;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasSpawned)
        {
            direct = player.position - transform.position;
            direct.Normalize();
            return;
        }
        spawn_timer += Time.deltaTime * Time.timeScale;
        if (spawn_timer < spawn_duration)
        {
            spawn_light.intensity = lightIntensity.Evaluate(spawn_timer / spawn_duration);
            spawn_Line.color = new Color(1, 1, 1, 1 - spawn_timer / spawn_duration);
            Dog_Sprite.GetComponent<SpriteRenderer>().color = Color.Lerp(start_Spawn_Color, Color.white, (spawn_timer - spawn_duration / 2) / (spawn_duration / 2));

        }
        else
        {
            spawn_light.gameObject.SetActive(false);
            spawn_Line.gameObject.SetActive(false);
            hasSpawned = true;
        }
    }

    private void FixedUpdate()
    {
        if (hasSpawned)
        {
            rb.velocity = direct * speed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var p = collision.gameObject.GetComponent<Player_Movement>();
        if (p != null)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direct * 1000);
            p.StopRunning();
            Destroy(gameObject);
        }
    }
}
