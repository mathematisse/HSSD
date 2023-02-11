using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Manager : MonoBehaviour
{
    Transform player;
    Rigidbody2D cameraRb;

    Vector3 vel;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.position, ref vel, 0f);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
