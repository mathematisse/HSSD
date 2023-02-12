using UnityEngine;
using UnityEngine.Rendering.Universal;

public class bat_movement : MonoBehaviour
{
    public Gradient cgrad;
    public float moveSpeed = 3;
    public float smoothTime = 0.5f;
    public bool isActive = false;
    public float followDistance = 1;
    public GameObject target;

    private GameObject followTarget;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Light2D coneLight;
    private bool isArrived = false;
    private BoxCollider2D boxCol;

    void Start()
    {
        boxCol= GetComponent<BoxCollider2D>();
        coneLight = GetComponentInChildren<Light2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    private void Update()
    {
        if (!isActive)
            return;
        animator.SetFloat("VelX", rb.velocity.x);
        animator.SetFloat("VelY", rb.velocity.y);
        FollowPlayer();
        if (!isArrived)
            SetLightGradient();
    }

    void FollowPlayer()
    {
        float distance = Vector2.Distance(transform.position, followTarget.transform.position);
        if (distance > followDistance)
        {
            float newSpeed = moveSpeed;
            if (!GetComponent<Renderer>().isVisible)
                newSpeed *= 10;
            direction = followTarget.transform.position - transform.position;
            rb.velocity = direction.normalized * newSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void SetLightGradient()
    {
        float distance = Vector2.Distance(transform.position, target.transform.position);
        coneLight.color = cgrad.Evaluate(1 / distance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive && !isArrived && collision.CompareTag("Player"))
        {
            isActive = true;
            animator.enabled = true;
            followTarget = collision.gameObject;
            boxCol.size = new Vector2(4, 4);
        }
        else if (!isArrived && collision.gameObject == target)
        {
            isArrived = true;
            followTarget = collision.gameObject;
            coneLight.color = Color.green;
        }
    }
}
