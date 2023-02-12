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
    public bool isRunning;
    private Time_Manager timeManager;
    private Animator animator;
    private TrailRenderer trailRenderer;
    private ParticleSystem _particleSystem_Trail;
    private ParticleSystem _particleSystem_Load;

    public AnimationCurve speedCurve;
    public float speedCurveTime = 1.0f;
    private float speedCurveTimer = 0.0f;
    private bool isPreparingToRun = false;
    private float realSpeed;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        timeManager = FindObjectOfType<Time_Manager>();
        trailRenderer = GetComponentsInChildren<TrailRenderer>()[0];
        var ps = GetComponentsInChildren<ParticleSystem>();
        _particleSystem_Trail = ps[0];
        _particleSystem_Load = ps[1];
    }

    void Update()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        direction.Normalize();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrepareToRun();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            StopRunning();
        }
        animator.SetFloat("VelX", rb.velocity.x);
        animator.SetFloat("VelY", rb.velocity.y);

        if (isPreparingToRun)
        {
            speedCurveTimer += Time.deltaTime;
            if (speedCurveTimer >= speedCurveTime && rb.velocity.magnitude > 1f)
            {
                StartRunning();
                Debug.Log("Start Running");
            }
            float curveValue = speedCurve.Evaluate(speedCurveTimer / speedCurveTime);
            realSpeed = Mathf.Lerp(moveSpeed, runSpeed, curveValue);
        }
        if (isRunning && rb.velocity.magnitude < 1f)
        {
            StopRunning();
            Debug.Log("Stop Running");
        }
    }

    private void FixedUpdate()
    {
        if (isRunning)
        {
            rb.velocity = direction * runSpeed;
        }
        else if (isPreparingToRun)
        {
            rb.velocity = direction * realSpeed;
        }
        else
        {
            rb.velocity = direction * moveSpeed;
        }
    }

    private void PrepareToRun()
    {
        if (isRunning || isPreparingToRun)
        {
            return;
        }
        isPreparingToRun = true;
        speedCurveTimer = 0.0f;
        timeManager.SetTimeScale(runTimeScale);
        _particleSystem_Load.Play();
    }

    private void StartRunning()
    {
        isPreparingToRun = false;
        isRunning = true;
        trailRenderer.emitting = true;
        _particleSystem_Trail.Play();
    }
    
    private void StopRunning()
    {
        if (!isRunning && !isPreparingToRun)
        {
            return;
        }
        isPreparingToRun = false;
        isRunning = false;
        trailRenderer.emitting = false;
        _particleSystem_Trail.Stop();
        _particleSystem_Load.Stop();
        timeManager.SetTimeScale(1.0f);
    }
}
