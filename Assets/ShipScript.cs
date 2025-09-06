using UnityEngine;

public class ShipScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed;
    public int health = 100;
    public int power = 10;
    public bool isMoving = false;
    public Vector2 destination;
    public GameObject destinationObject;
    public int maxDist = 2;
    public int dist = 2;
    private Vector3 offset;
    public float offsetx = 0;
    public float offsety = 0;

    //obiectul unde stationeaza acum nava (obiect tip star/blackhole sau muchie)
    public StarScript currentStar = null;
    public StarScript targetStar = null;

    void Start()
    {
        offset.x = offsetx;
        offset.y = offsety;
        offset.z = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void move()
    {
        if (dist != 0)
        {
            currentStar = targetStar;
            transform.position = currentStar.transform.position + offset;
            dist -= 1;
        }
    }
}
