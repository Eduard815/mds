using UnityEngine;

public class SaveGalaxy : MonoBehaviour
{
    public static SaveGalaxy Instance; //singleton, we gotta keep our same galaxy when switching between SampleScene and SolarSystem
    public int galaxySeed;

    //seed and type for selected star
    public int selectedStarSeed;
    public StarType selectedStarType;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void NewGame()
    {
        galaxySeed = Random.Range(int.MinValue, int.MaxValue);
        selectedStarSeed = 0;
        Debug.Log("Seed " + galaxySeed);
    }
}

