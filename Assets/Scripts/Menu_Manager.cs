using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Manager : MonoBehaviour
{
    Button button_Start;
    Button button_Quit;
    
    // Start is called before the first frame update
    void Start()
    {
        var bts = GetComponentsInChildren<Button>();
        button_Start = bts[0];
        button_Quit = bts[1];
        button_Start.onClick.AddListener(StartGame);
        button_Quit.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
