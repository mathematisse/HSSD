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
    private Animator animator;
    private ParticleSystem _particleSystem;
    private bool isDead = false;
    private float death_timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        spawn_light = GetComponentsInChildren<Light2D>()[0];
        var sr = GetComponentsInChildren<SpriteRenderer>();
        Dog_Sprite = sr[0];
        spawn_Line = sr[1];
        Dog_Sprite.color = start_Spawn_Color;
        rb = GetComponent<Rigidbody2D>();
        _particleSystem = GetComponentsInChildren<ParticleSystem>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            death_timer -= Time.deltaTime;
            if (death_timer < 0)
            {
                Destroy(gameObject);
            }
            return;
        }
        if (hasSpawned)
        {
            direct = player.position - transform.position;
            direct.Normalize();
            animator.SetFloat("VelX", direct.x);
            animator.SetFloat("VelY", direct.y);
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
        if (hasSpawned && !isDead)
        {
            rb.velocity = direct * speed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
        {
            return;
        }
        var _rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (_rb != null)
        {
            _rb.AddForce(direct * 1000f);
        }
        var p = collision.gameObject.GetComponent<Player_Movement>();
        if (p != null)
        {
            p.StopRunning();
        }
        isDead = true;
        Dog_Sprite.gameObject.SetActive(false);
        death_timer = 1f;
        _particleSystem.Play();
    }
}
