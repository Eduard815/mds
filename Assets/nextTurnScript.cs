using UnityEngine;

public class nextTurnScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    bool nextTurn = false;
    public Transform shipsPointer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)){
            if(!nextTurn){
                nextTurn = true;
            }
        }
        if(nextTurn){
            foreach(Transform child in shipsPointer){
                ShipScript ship = child.GetComponent<ShipScript>();
                if(ship.isMoving){
                    Vector2 nextPos;
                    Vector2 currentPosition = new Vector2(child.position.x, child.position.y);
                    Vector2 direction = ship.destination - currentPosition;
                    float distanceToTarget = direction.magnitude;
                    if(distanceToTarget <= ship.speed){
                        nextPos = ship.destination;
                    }
                    else{
                        Vector2 step = direction.normalized*ship.speed;
                        nextPos = currentPosition + step;
                    }
                    child.position = new Vector3(nextPos.x, nextPos.y, child.position.z);
                }
                Debug.Log(child.name);
            }
            nextTurn = false;
        }
    }
}
