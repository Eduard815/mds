using UnityEngine;

public class ShipScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed;
    public bool isMoving = false;
    public Vector2 destination;
    public GameObject destinationObject;

    //obiectul unde stationeaza acum nava (obiect tip star/blackhole sau muchie)
    public StarScript currentStar;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void move()
    {
        transform.position = currentStar.transform.position;
    }
}
