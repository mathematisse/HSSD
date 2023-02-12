using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingSound : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    void Start()
    {
        soundManager.instance.PlayMusic(_clip);
    }
}
