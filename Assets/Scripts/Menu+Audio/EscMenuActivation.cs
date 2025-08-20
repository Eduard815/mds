using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscMenuActivation : MonoBehaviour
{
    public GameObject EscMenuPanel;
    public Button resume, savegame, loadgame, options, mainmenu;
    bool esc_menu_open = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //if we press that esc
        {
            if (esc_menu_open == false)
            {
                EscMenuPanel.SetActive(true);
                esc_menu_open = true;
                Time.timeScale = 0; //effectively pauses the game
            }
            else
            {
                EscMenuPanel.SetActive(false);
                esc_menu_open = false;
                Time.timeScale = 1; //unfreezes the game
            }
        }
    }

    public void Resume()
    {
        EscMenuPanel.SetActive(false);
        esc_menu_open = false;
        Time.timeScale = 1; //unfreezes game time, happening at normal rate, that is 1
    }
    public void Save()
    {
        Debug.Log("We got to implement the saving, man");
    }
    public void Load()
    {
        Debug.Log("We got to implement the loading, man");
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        esc_menu_open = false;
        SceneManager.LoadScene(0); //sends you back to scene 0
    }
}
