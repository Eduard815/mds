using UnityEngine;
using UnityEngine.SceneManagement;

public class SolarSystemUI : MonoBehaviour
{
    public void BackToGalaxy()
    {
        SaveSolarSystem();
        SceneManager.LoadScene("SampleScene");
    }

    public void SaveSolarSystem()
    {
        Debug.Log("SolarSystem saved with star id: " + SaveGalaxy.Instance.selectedStarId);
    }
}