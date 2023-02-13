using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ_manager : MonoBehaviour
{
    public GameObject pnj1;
    public GameObject pnj2;

    // Start is called before the first frame update
    void Start()
    {
        pnj2.SetActive(false);
    }

    public void Get_Bat()
    {
        pnj1.SetActive(false);
        pnj2.SetActive(true);
    }
    
}
