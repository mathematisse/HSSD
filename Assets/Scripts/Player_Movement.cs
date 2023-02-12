using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField]
    [Range(1f, 10f)]
    public float moveSpeed = 5.0f;
    
    [SerializeField]
    [Range(10f, 20f)]
    public float runSpeed = 15.0f;
    
    [SerializeField]
    [Range(0.2f, 1.0f)]
    public float runTimeScale = 0.5f;
    
    [Space]
    
    [Header("Transition To Running")]
    [SerializeField]
    public AnimationCurve speedCurve;
    [SerializeField]
    [Range(0.1f, 3.0f)]
    public float speedCurveTime = 1.0f;

    [HideInInspector]
    public bool isRunning;
    
    private Animator animator;
    private Rigidbody2D rb;
    private Time_Manager timeManager;
    private TrailRenderer trailRenderer;
    private TrailRenderer bloodTrailRenderer;
    private ParticleSystem _particleSystem_Trail;
    private ParticleSystem _particleSystem_Load;

    private float speedCurveTimer = 0.0f;
    private bool isPreparingToRun = false;
    private float blood_trail_duration = 0.2f;
    private float blood_trail_timer = 0.0f;
    
    private float realSpeed;
    private Vector2 direction;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        timeManager = FindObjectOfType<Time_Manager>();
        var ts = GetComponentsInChildren<TrailRenderer>();
        trailRenderer = ts[0];
        bloodTrailRenderer = ts[1];
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
        if (bloodTrailRenderer.emitting)
        {
            blood_trail_timer += Time.deltaTime * Time.timeScale;
            if (blood_trail_timer >= blood_trail_duration)
            {
                bloodTrailRenderer.emitting = false;
            }
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

    public void IKilledSomeone()
    {
        Debug.Log("Must have hurt");
        blood_trail_timer = 0f;
        bloodTrailRenderer.emitting = true;
    }
}
