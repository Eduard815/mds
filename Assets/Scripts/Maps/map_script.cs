using UnityEngine;
using System.Collections.Generic;
using System.Linq;                      /// Pentru Enumerable
using Random = UnityEngine.Random;



public class PoissonSampler : MonoBehaviour
{
    public GameObject[] starPrefabs;
    public float[] starProb;
    public GameObject edgeLinePrefab;
    float r = 2.5f * Mathf.Sqrt(2f);
    int k = 30;
    float areaSize = 60f;
    float smallAreaSize = 16f;
    float cellSize;
    int gridWidth, gridHeight;
    int starIndex = 0;

    private int idIndex = 0;
    public List<GameObject> instantiatedStars;
    Vector2 empty = new Vector2(99999f, 99999f);
    int[,] grid;
    List<Vector2> points = new List<Vector2>();
    List<Vector2> active = new List<Vector2>();


    List<(int a, int b, float cost)> edges = new List<(int, int, float)>();


    /// variabile Kruskal
    int[] T;
    List<(int a, int b, float cost)> res = new List<(int, int, float)>();

    /// Pentru generare de cicluri
    Dictionary<(int, int), bool> inEdges = new Dictionary<(int, int), bool>();






    void Start()
    {
        //generating the galaxy with the seed and saving it between scenes switching
        Random.InitState(SaveGalaxy.Instance.galaxySeed);


        GameObject mapGO = GameObject.Find("Map");


        /////////////////////////////// Linie pe diamteru pentru debugging
        /*
        Vector2 a = new Vector2(-15f, 0f);
        Vector2 b = new Vector2(15f, 0f);

        GameObject gameObj = Instantiate(edgeLinePrefab);
        LineRenderer lr = gameObj.GetComponent<LineRenderer>();
        lr.useWorldSpace = true;

        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.positionCount = 2;

        lr.SetPosition(0, new Vector3(a.x, a.y, 0));
        lr.SetPosition(1, new Vector3(b.x, b.y, 0));
        */
        ///////////////////////////////





        /////////////////////////////// Stea centrala pentru debugging
        /*
        Vector2 pos = new Vector2(0f, 0f);
        var testStar = Instantiate(starPrefab, pos, Quaternion.identity, mapGO.transform);
        Debug.Log($"Test star at coordinates {testStar.transform.position}");
        */
        ///////////////////////////////

        cellSize = r / Mathf.Sqrt(2);
        gridWidth = Mathf.CeilToInt(areaSize / cellSize) + 1;
        gridHeight = Mathf.CeilToInt(areaSize / cellSize) + 1;

        grid = new int[gridHeight, gridWidth];
        for (int i = 0; i < gridHeight; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                grid[i, j] = -1;
            }
        }

        Vector2 firstStar;
        do
        {
            float x = Random.Range(-areaSize / 2, areaSize / 2);
            float y = Random.Range(-areaSize / 2, areaSize / 2);
            firstStar = new Vector2(x, y);
        } while (firstStar.magnitude >= areaSize / 2 || firstStar.magnitude < smallAreaSize / 2);

        Insert(firstStar);

        while (active.Count > 0)
        {
            int randomIndex = Random.Range(0, active.Count);
            Vector2 randomActive = active[randomIndex];

            bool candidateFound = false;
            for (int attempt = 0; attempt < k; attempt++)
            {
                float randomRadius = Random.Range(r, 2 * r);
                float degrees = Random.Range(0f, 360f);
                float radians = degrees * Mathf.Deg2Rad;

                Vector2 candidate = randomActive + new Vector2(randomRadius * Mathf.Cos(radians), randomRadius * Mathf.Sin(radians));

                if (StarValidation(candidate))
                {
                    Insert(candidate);
                    candidateFound = true;
                    break;
                }
            }
            if (!candidateFound)
            {
                active.RemoveAt(randomIndex);
            }
        }


        /// Debug pentru prima stea
        /// Vector2 star = points[0];
        /// GameObject go = Instantiate(starPrefab, new Vector3(star.x, star.y, 0f), Quaternion.identity, mapGO.transform);
        
        //modified in order to add starType, starSeed update
        for (int i = 0; i < points.Count; i++)
        {
            /// Instantiate(starPrefab, star, Quaternion.identity);
            Vector2 star = points[i];
            var starPrefab = RandomStarPrefab();

            GameObject go = Instantiate(starPrefab, new Vector3(star.x, star.y, 0f), Quaternion.identity, mapGO.transform);
            StarScript starComp = go.GetComponent<StarScript>();

            starComp.id = i;
            if (starPrefab.name.Contains("red")) starComp.type = StarType.Red;
            else if (starPrefab.name.Contains("yellow")) starComp.type = StarType.Yellow;
            else if (starPrefab.name.Contains("white")) starComp.type = StarType.White;
            else if (starPrefab.name.Contains("blue")) starComp.type = StarType.Blue;
            else if (starPrefab.name.Contains("blackhole")) starComp.type = StarType.BlackHole;

            starComp.seed = SeedGeneration.GenSeed(SaveGalaxy.Instance.galaxySeed, i);

            instantiatedStars.Add(go);
            /// go.transform.position = new Vector3(star.x, star.y, 0f);
        }
        Debug.Log(points.Count);





        int nrEdges = points.Count;
        T = new int[nrEdges];
        for (int i = 0; i < nrEdges; i++)
        {
            T[i] = i;
        }

        AllEdges();
        Kruskal();
        CreateEdges();
        AddCycles();
        LinkNeighbours();
        StarScript lastStar = instantiatedStars[instantiatedStars.Count - 1].GetComponent<StarScript>();
        ShipScript ship = FindFirstObjectByType<ShipScript>();
        ship.targetStar = lastStar;
        ship.move();
        /// Debug.Log($"Muchii create inainte de cicluri: {res.Count}");
    }


    void Insert(Vector2 star)
    {
        int iGrid = gridHeight - 2 - Mathf.FloorToInt((areaSize / 2 + star.y) / cellSize);
        int jGrid = Mathf.FloorToInt((areaSize / 2 + star.x) / cellSize);
        grid[iGrid, jGrid] = starIndex;
        starIndex++;

        points.Add(star);
        active.Add(star);
    }


    bool StarValidation(Vector2 star)
    {
        /* /// Vechiul patrat
        if (star.x < -areaSize / 2f || star.x >= areaSize / 2f || star.y < -areaSize / 2f || star.y >= areaSize / 2f){
            return false;
        }
        */
        /// Noul cerc
        if (star.magnitude >= areaSize / 2 || star.magnitude < smallAreaSize / 2) return false;

        int iGrid = gridHeight - 2 - Mathf.FloorToInt((areaSize / 2 + star.y) / cellSize);
        int jGrid = Mathf.FloorToInt((areaSize / 2 + star.x) / cellSize);

        if (iGrid < 0 || iGrid >= gridHeight || jGrid < 0 || jGrid >= gridWidth)
        {
            return false;
        }

        for (int i = -1; i <= 1; i++)
        {
            int ii = iGrid + i;
            for (int j = -1; j <= 1; j++)
            {
                int jj = jGrid + j;
                if (ii >= 0 && ii < gridHeight && jj >= 0 && jj < gridWidth && grid[ii, jj] != -1)
                {
                    float distance = Mathf.Sqrt(Mathf.Pow(points[grid[ii, jj]].x - star.x, 2) + Mathf.Pow(points[grid[ii, jj]].y - star.y, 2));
                    if (distance < r)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }


    GameObject RandomStarPrefab()
    {
        float rand = Random.value;
        for (int i = 0; i < starPrefabs.Length; i++)
        {
            if (rand < starProb[i])
            {
                return starPrefabs[i];
            }
        }
        return starPrefabs[starPrefabs.Length - 1];
    }





    void AllEdges()
    {
        foreach (var star in points)
        {
            int iGrid = gridHeight - 2 - Mathf.FloorToInt((areaSize / 2 + star.y) / cellSize);
            int jGrid = Mathf.FloorToInt((areaSize / 2 + star.x) / cellSize);

            for (int i = -2; i <= 2; i++)
            {
                int ii = iGrid + i;
                for (int j = -2; j <= 2; j++)
                {
                    int jj = jGrid + j;
                    if (ii >= 0 && ii < gridHeight && jj >= 0 && jj < gridWidth && grid[ii, jj] != -1)
                    {
                        float distance = Mathf.Sqrt(Mathf.Pow(points[grid[ii, jj]].x - star.x, 2) + Mathf.Pow(points[grid[ii, jj]].y - star.y, 2));
                        edges.Add((grid[iGrid, jGrid], grid[ii, jj], distance));
                    }
                }
            }
        }
    }



    int Root(int v)
    {
        if (T[v] == v)
        {
            return v;
        }
        return T[v] = Root(T[v]);
    }

    void Union(int u, int v)
    {
        u = Root(u);
        v = Root(v);
        if (u != v)
        {
            T[v] = u;
        }
    }

    void Kruskal()
    {
        edges.Sort((e1, e2) => e1.cost.CompareTo(e2.cost));
        foreach (var (u, v, cost) in edges)
        {
            int a = Root(u);
            int b = Root(v);
            if (a != b)
            {
                Union(a, b);
                res.Add((u, v, cost));
            }
        }
    }


    static void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }


    void AddCycles()
    {
        GameObject mapGO = GameObject.Find("Map");

        int minSelected = (int)points.Count / 4;
        int maxSelected = (int)points.Count / 2;
        int selected = Random.Range(minSelected, maxSelected + 1);
        List<int> starIndexes = Enumerable.Range(0, points.Count).ToList();
        Shuffle(starIndexes);

        for (int ind = 0; ind < selected; ind++)
        {
            int edgesToGenerate;
            float rand = Random.value;
            if (rand < 0.1f)
            {
                edgesToGenerate = 0;
            }
            else if (rand < 0.2f)
            {
                edgesToGenerate = 1;
            }
            else if (rand < 0.3f) edgesToGenerate = 2;
            else edgesToGenerate = 3;

            int currentStarId = starIndexes[ind];
            var star = points[currentStarId];
            int iGrid = gridHeight - 2 - Mathf.FloorToInt((areaSize / 2 + star.y) / cellSize);
            int jGrid = Mathf.FloorToInt((areaSize / 2 + star.x) / cellSize);

            int attempts = 0;
            while (edgesToGenerate > 0 && attempts < 30)
            {
                attempts++;

                /// Iau un punct random din vecinatatea celui curent
                int iiRand = Random.Range(-1, 2);
                int jjRand = Random.Range(-1, 2);
                int ii = iGrid + iiRand;
                int jj = jGrid + jjRand;

                /// Verific ca e in grid si exista ca si punct
                if (ii < 0 || ii >= gridHeight || jj < 0 || jj >= gridWidth || grid[ii, jj] == -1)
                {
                    continue;
                }
                int neighborId = grid[ii, jj];
                if (neighborId == -1) continue;

                /// Daca exista deja cheia in dictionar
                if (inEdges.ContainsKey((currentStarId, neighborId)) || inEdges.ContainsKey((neighborId, currentStarId)))
                {
                    continue;
                }


                /// Bag muchia si o instantiez
                inEdges[(currentStarId, neighborId)] = true;
                inEdges[(neighborId, currentStarId)] = true;

                GameObject lineObj = Instantiate(edgeLinePrefab, mapGO.transform);
                LineRenderer lineRenderer = lineObj.GetComponent<LineRenderer>();
                lineRenderer.useWorldSpace = true;

                lineRenderer.positionCount = 2;
                lineRenderer.startWidth = 0.04f;
                lineRenderer.endWidth = 0.04f;

                lineRenderer.SetPosition(0, new Vector3(points[currentStarId].x, points[currentStarId].y, 0f));
                lineRenderer.SetPosition(1, new Vector3(points[neighborId].x, points[neighborId].y, 0f));

                // Add the actual edge between the two stars to res list
                float distance = Vector2.Distance(points[currentStarId], points[neighborId]);
                res.Add((currentStarId, neighborId, distance));
                edgesToGenerate--;
            }
        }
    }




    void CreateEdges()
    {
        /*
        GameObject container = new GameObject("Edges");
        container.transform.position = Vector3.zero;
        */

        GameObject mapGO = GameObject.Find("Map");


        foreach (var edge in res)
        {
            GameObject lineObj = Instantiate(edgeLinePrefab, mapGO.transform);
            LineRenderer lineRenderer = lineObj.GetComponent<LineRenderer>();
            lineRenderer.useWorldSpace = true;

            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = 0.04f;
            lineRenderer.endWidth = 0.04f;

            lineRenderer.SetPosition(0, new Vector3(points[edge.a].x, points[edge.a].y, 0f));
            lineRenderer.SetPosition(1, new Vector3(points[edge.b].x, points[edge.b].y, 0f));

            inEdges[(edge.a, edge.b)] = true;
            inEdges[(edge.b, edge.a)] = true;

            // StarScript starA = mapGO.transform.GetChild(edge.a).GetComponent<StarScript>();
            // StarScript starB = mapGO.transform.GetChild(edge.b).GetComponent<StarScript>();

            // starA.neighbours.Add(starB);

            // starB.neighbours.Add(starA);

            Debug.Log($"Edge {edge.a} -> {edge.b} instantiated at {lineObj.transform.position}");
        }
    }
    void LinkNeighbours()
    {
        GameObject mapGO = GameObject.Find("Map");

        foreach (var edge in res)
        {
            StarScript starA = mapGO.transform.GetChild(edge.a).GetComponent<StarScript>();
            StarScript starB = mapGO.transform.GetChild(edge.b).GetComponent<StarScript>();

            starA.neighbours.Add(starB);

            starB.neighbours.Add(starA);
        }
    }


}

