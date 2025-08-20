using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string MapScene = "SampleScene";
    public void NewGame()
    {
        //loading SampleScene (the map scene)
        //Also calling SaveGalaxy for Galaxy seed generation
        if (SaveGalaxy.Instance != null) SaveGalaxy.Instance.NewGame();
        SceneManager.LoadScene(MapScene);
    }

    public void QuitGame()
{
    #if UNITY_EDITOR
        // Application.Quit() not working, set isPlaying to false and it'll be working
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
}
}