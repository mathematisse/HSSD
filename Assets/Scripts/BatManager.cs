using UnityEngine;
using UnityEngine.Rendering.Universal;

public class bat_movement : MonoBehaviour
{
    public Gradient cgrad;
    public float moveSpeed = 3;
    public float smoothTime = 0.5f;
    public bool isActive = false;
    public float followDistance = 1;
    public GameObject destination;

    private GameObject trip_companion;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Light2D coneLight;
    private bool isArrived = false;
    private BoxCollider2D boxCol;
    private float to_trip_companion;
    private float to_destination;
    
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
        to_trip_companion = Vector2.Distance(transform.position, trip_companion.transform.position);
        to_destination = Vector2.Distance(transform.position, destination.transform.position);
        FollowPlayer();
        if (!isArrived)
            SetLightGradient();
        if (!isArrived && to_destination < 5f)
        {
            isArrived = true;
            trip_companion = destination;
            coneLight.color = Color.green;
        }
    }

    void FollowPlayer()
    {
        if (to_trip_companion > followDistance)
        {
            float newSpeed = moveSpeed;
            if (!GetComponent<Renderer>().isVisible)
                newSpeed *= 10;
            direction = trip_companion.transform.position - transform.position;
            rb.velocity = direction.normalized * newSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void SetLightGradient()
    {
        coneLight.color = cgrad.Evaluate(1 / to_destination);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive && !isArrived && collision.CompareTag("Player"))
        {
            isActive = true;
            animator.enabled = true;
            trip_companion = collision.gameObject;
            boxCol.size = new Vector2(4, 4);
        }
        
    }
}
