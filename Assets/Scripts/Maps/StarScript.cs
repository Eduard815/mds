using UnityEngine;
using System.Collections.Generic;

//scriptul va fi atasat fiecerei stele/blackhole

public class StarScript : MonoBehaviour
{
    /*tipul obiectului:
    0 - black hole
    1 - star
    */
    //id->indexul punctului corespunzator acestui obiect in lista din map_script
    public int id;
    public List<StarScript> neighbours = new();
    public int type;
    public string owner = "none";
    //ce cantitate de resurse contine obiectul
    public int gas;
    public int metal;
    void Start()
    {
        if (type != 0)
        {
            if (Random.Range(0, 100) > 50) // probabilitate de 50% ca sistemul sa aiba civilizatii
            {
                owner = "Unknown civilization";
            }    
            if (Random.Range(0, 100) > 75) //probabilitate de 25% ca obiectul sa contina ambele tipuri de resurse
            {
                gas = Random.Range(5, 20);
                metal = Random.Range(5, 20);
            }
            else if (Random.Range(0, 100) > 50)
            {
                gas = Random.Range(5, 20);
                metal = 0;
            }
            else
            {
                gas = 0;
                metal = Random.Range(5, 20);
            }
        }
    }

    void Update()
    {

    }
}