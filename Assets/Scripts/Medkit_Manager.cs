using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit_Manager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player" && GetComponent<Renderer>().enabled)
        {
            var player = other.gameObject.GetComponent<Player_Manager>();
            player.onHeal();
            GetComponent<Renderer>().enabled = false;
        }
    }
}