using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioScene2 : MonoBehaviour
{
    [SerializeField] private GameObject[] menus_with_buttons; //array of fields in inspector for both options menu and simple menu
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager audio2 = FindObjectOfType<AudioManager>(); //we need this reference to AudioManager to call functions from AudioManager (ClickSound)
        foreach (GameObject menu in menus_with_buttons) //for every menu with buttons
        {
            Button[] buttons = menu.GetComponentsInChildren<Button>(true); //THIS FIXED EVERYTHING - IT MADE THE BUTTONS IN HIDDEN OBJECTS WORK
            foreach (Button b in buttons)
            {
                b.onClick.AddListener(audio2.ClickSound); 
            }
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
