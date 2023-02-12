using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_gyro : MonoBehaviour
{
    public float speed = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        float angleMove = speed * Time.deltaTime * Time.timeScale;
        transform.Rotate(0, 0, angleMove);
    }
}
