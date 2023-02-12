using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood_Manager : MonoBehaviour
{
    public GameObject bloodDrop;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void DoBloodCollision(Collision2D collision)
    {
        for (int i = 0; i < 10; i++)
        {
            CreateBloodDropInRandomDirection(collision.relativeVelocity.normalized);
        }
        Destroy(this.gameObject);
    }
    
    void CreateBloodDropInRandomDirection(Vector2 relvel)
    {
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized + relvel;
        GameObject bloodDropInstance = Instantiate(bloodDrop, transform.position, Quaternion.identity);
        bloodDropInstance.GetComponent<Rigidbody2D>().AddForce(randomDirection * 500f);
    }
}