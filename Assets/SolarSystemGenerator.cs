using UnityEngine;
using System.Collections.Generic;

public class SolarSystemGenerator : MonoBehaviour
{
    [Header("Star Sprites")]
    public Sprite redStar;
    public Sprite yellowStar;
    public Sprite whiteStar;
    public Sprite blueStar;
    public Sprite blackHole;

    [Header("Planet Sprites")]
    public Sprite[] planetSprites;

    [Header("Custom Planet Sprites")]
    public Sprite liveablePlanet;
    public Sprite gasGiant;
    public Sprite icePlanet;
    public Sprite desertPlanet;

    [Header("Layout")]
    public Transform root;
    public int minPlanets = 3;
    public int maxPlanets = 6;
    public float firstOrbit = 4.2f;
    public float orbitStep = 3.7f;


    [Header("Star Scale")]
    public float redStarScale = 0.4f;
    public float yellowStarScale = 0.1f;
    public float whiteStarScale = 0.1f;
    public float blueStarScale = 0.55f;
    public float blackHoleScale = 1.1f;

    [Header("Planet Scale")]
    public float planetScaleMin = 0.5f;
    public float planetScaleMax = 0.55f;
    public float liveablePlanetFactor = 0.065f;
    public float gasGiantFactor = 1f;
    public float icePlanetFactor = 0.4f;
    public float desertPlanetFactor = 0.25f;

    void Start()
    {
        if (root == null) root = this.transform;

        int seed = SaveGalaxy.Instance.selectedStarSeed; //save solarsystem through scene change
        StarType type = SaveGalaxy.Instance.selectedStarType;
        Random.InitState(seed); //all random values will be the same for the star with this seed

        var starGameObject = new GameObject("Star");
        var starSpriteRenderer = starGameObject.AddComponent<SpriteRenderer>();
        starSpriteRenderer.sprite = GetStarSprite(type);
        starSpriteRenderer.sortingOrder = 10; //placing of the sprite
        starGameObject.transform.SetParent(root, false);
        starGameObject.transform.localPosition = Vector3.zero; //star is now in the center
        starGameObject.transform.localScale = Vector3.one * GetStarScale(type);

        //black holes can't have palnets orbiting them
        if (type == StarType.BlackHole) return;

        //planet generation
        int planetNumber = Random.Range(minPlanets, maxPlanets + 1);
        for (int i = 0; i < planetNumber; i++)
        {
            float radius = firstOrbit + i * orbitStep;
            float planetAngle = Random.Range(0f, 360f);

            //calculating planet's position with radians (Deg2Rad)
            Vector3 planetPosition = new Vector3(Mathf.Cos(planetAngle * Mathf.Deg2Rad) * radius, Mathf.Sin(planetAngle * Mathf.Deg2Rad) * radius, 0f);

            

            //planets generation
            var planetGameObject = new GameObject($"Planet_{i}");
            var planetSpriteRenderer = planetGameObject.AddComponent<SpriteRenderer>();
            Sprite currentPlanetSprite = planetSprites[Random.Range(0, planetSprites.Length)];
            planetSpriteRenderer.sprite = currentPlanetSprite;
            planetSpriteRenderer.sortingOrder = 9;

            planetGameObject.transform.SetParent(root, false);
            planetGameObject.transform.localPosition = planetPosition;


            //planet scaling
            float planetScale = Random.Range(planetScaleMin, planetScaleMax);
            if (currentPlanetSprite == liveablePlanet)
            {
                planetScale *= liveablePlanetFactor;
            }
            if (currentPlanetSprite == gasGiant)
            {
                planetScale *= gasGiantFactor;
            }
            if (currentPlanetSprite == icePlanet)
            {
                planetScale *= icePlanetFactor;
            }
            if (currentPlanetSprite == desertPlanet)
            {
                planetScale *= desertPlanetFactor;
            }
            planetGameObject.transform.localScale = Vector3.one * planetScale;
        }

        //getters
        Sprite GetStarSprite(StarType st)
        {
            switch (st)
            {
                case StarType.Red: return redStar;
                case StarType.Yellow: return yellowStar;
                case StarType.White: return whiteStar;
                case StarType.Blue: return blueStar;
                case StarType.BlackHole: return blackHole;
                default: return redStar;
            }
        }

        float GetStarScale(StarType st)
        {
            switch (st)
            {
                case StarType.Red: return redStarScale;
                case StarType.Yellow: return yellowStarScale;
                case StarType.White: return whiteStarScale;
                case StarType.Blue: return blueStarScale;
                case StarType.BlackHole: return blackHoleScale;
                default: return redStarScale;
            }
        }
    }

    
    void Update()
    {
        
    }
}
