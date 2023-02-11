using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_Manager : MonoBehaviour
{
    public float timeScale = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeScale;
    }

    public void SetTimeScale(float scale)
    {
        timeScale = scale;
    }
}
