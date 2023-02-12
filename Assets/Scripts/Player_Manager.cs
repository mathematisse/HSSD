using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Manager : MonoBehaviour
{
    public int killCounter = 0;

    public bool showKillsInHud = true;

    public int hearts = 8;

    [Range(1f, 100f)]
    public float maxMana = 100f;

    public float fullManaDurationInSeconds = 30f;

    private float manaConsomation;
    
    private float mana;

    public TMP_Text killCounterOutput;
    public Slider heartsOutput;
    public Slider manaOutput;

    private Player_Movement movements;

    public bool canRun = true;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        movements = GetComponent<Player_Movement>();
        manaConsomation = 100f / fullManaDurationInSeconds;
        mana = maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        if (movements.isRunning) {
            mana = mana - manaConsomation * Time.deltaTime;
            if (mana <= 0) {
                canRun = false;
                mana = 0;
            }
            Debug.Log("mana = " + mana);
            manaOutput.value = mana / 100f;
        }
    }

    public void onHeal()
    {
        if (isDead) return;
        hearts++;
        heartsOutput.value = hearts;
    }

    public void onDrug()
    {
        canRun = true;
        mana = maxMana;
        manaOutput.value = mana / 100f;
    }

    public void onDamage()
    {
        if (hearts == 0) return;
        hearts--;
        if (hearts == 0) isDead = true;
        heartsOutput.value = hearts;
    }

    public void onKill()
    {
        killCounter++;
        if (killCounterOutput != null && showKillsInHud) killCounterOutput.text = getKillString();
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
