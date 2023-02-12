using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drug_Manager : MonoBehaviour
{

    public float respawnDuration = 20f;

    private float respawnIn = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Renderer>().enabled) {
            respawnIn -= Time.deltaTime;
            if (respawnIn <= 0) GetComponent<Renderer>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player" && GetComponent<Renderer>().enabled)
        {
            var player = other.gameObject.GetComponent<Player_Manager>();
            player.onDrug();
            respawnIn = respawnDuration;
            GetComponent<Renderer>().enabled = false;
        }
    }
}