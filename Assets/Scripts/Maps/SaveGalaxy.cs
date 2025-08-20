using UnityEngine;

public class SaveGalaxy : MonoBehaviour
{
    public static SaveGalaxy Instance; //singleton, we gotta keep our same galaxy when switching between SampleScene and SolarSystem
    public int galaxySeed;

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
        Debug.Log("Seed " + galaxySeed);
    }
}

