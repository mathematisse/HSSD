using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Object : MonoBehaviour
{
    public float time_for_dog_spawn = 5f;
    private float dog_timer;
    public GameObject Dog;
    Player_Movement player;
    private float spawn_dist = 5f;
    private Vector2 rdm_Dir;
    private float rdm_Dist;
    private Vector2 to_rdm_Dir;
    private Rigidbody2D rb;
    private float speed = 1f;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player_Movement>();
        dog_timer = time_for_dog_spawn;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move_Rdm_Values();
        if (dog_timer > 0)
        {
            dog_timer -= Time.deltaTime;
        }
        else
        {
            dog_timer = time_for_dog_spawn;
            Spawn_Dog_Randomly();
        }
        animator.SetFloat("VelX", rb.velocity.x);
        animator.SetFloat("VelY", rb.velocity.y);

    }

    void Spawn_Dog_Randomly()
    {
        var pos = new Vector3(Random.Range(-spawn_dist, spawn_dist), Random.Range(-spawn_dist, spawn_dist), 0);
        var dog = Instantiate(Dog, pos + transform.position, Quaternion.identity, transform).GetComponent<Dog_Manager>();
        dog.player = player.transform;
    }

    private void Move_Rdm_Values()
    {
        rdm_Dir += new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)) ;
        rdm_Dir.Normalize();
        rdm_Dist += Random.Range(-1, 1);
        if (rdm_Dist > 15)
        {
            rdm_Dist = 15;
        }
        else if (rdm_Dist < 5)
        {
            rdm_Dist = 5;
        }
    }

    private void FixedUpdate()
    {
        var pos = player.transform.position + (Vector3)rdm_Dir * rdm_Dist;
        to_rdm_Dir = pos - transform.position;
        to_rdm_Dir.Normalize();
        rb.AddForce(to_rdm_Dir * speed);
        //rb.velocity = ((to_rdm_Dir * speed) + rb.velocity) / 2f;
        Debug.DrawLine(transform.position, pos, Color.red);
    }
}
