using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light_Blinker : MonoBehaviour
{
    public AnimationCurve curve;
    public float duration = 1.0f;
    public float timer = 0.0f;

    Light2D mainNeon;
    Light2D hallo;
    
    // Start is called before the first frame update
    void Start()
    {
        mainNeon = GetComponent<Light2D>();
        hallo = GetComponentsInChildren<Light2D>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < duration)
        {
            timer += Time.deltaTime * Time.timeScale;
            float t = timer / duration;
            float intensity = curve.Evaluate(t);
            mainNeon.intensity = intensity;
            hallo.intensity = intensity;
        }
        else
        {
            timer = 0.0f;
        }
    }
}
