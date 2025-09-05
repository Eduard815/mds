using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CircleCollider2D))] //for mouse click detection (mousedown)
public class Star : MonoBehaviour
{
    public int seed;
    public StarType type;

    //on click, we save star's seed and type and load the SolarSystem scene
    void OnMouseDown()
    {
        Debug.Log($"[Star] Click on star: seed = {seed}, type = {type}");
        SaveGalaxy.Instance.selectedStarSeed = seed;
        SaveGalaxy.Instance.selectedStarType = type;
        SceneManager.LoadScene("SolarSystem");
    }
}