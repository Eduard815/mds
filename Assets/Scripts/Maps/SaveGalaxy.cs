using UnityEngine;

public class SaveGalaxy : MonoBehaviour
{
    public static SaveGalaxy Instance; //singleton, we gotta keep our same galaxy when switching between SampleScene and SolarSystem
    public int galaxySeed;

    public int selectedStarId;
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
        Debug.Log("Galaxy Seed " + galaxySeed);

        //erase all data about the last selected star system
        selectedStarId = -1;
        selectedStarSeed = -1;
        
    }
}

