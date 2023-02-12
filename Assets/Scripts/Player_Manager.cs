using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Manager : MonoBehaviour
{
    public int killCounter = 0;
    public TMP_Text killCounterOutput;

    public bool showKillsInHud = true;

    public int hearts = 8;

    [Range(1f, 100f)]
    public float mana = 100f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onKill()
    {
        killCounter++;
        if (killCounterOutput != null) killCounterOutput.text = getKillString();
    }

    string getKillString()
    {
        if (killCounter < 10)
        {
            return "0" + killCounter;
        }
        if (killCounter < 100)
        {
            return "" + killCounter;
        }
        return "XX";
    }
}
