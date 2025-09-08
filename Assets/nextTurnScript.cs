using UnityEngine;

public class nextTurnScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    bool nextTurnBtn = false;
    bool nextTurn = false;
    public Transform shipsPointer;
    void Start()
    {
        
    }

    public void onNextTurnButton(){
        nextTurn = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!nextTurnBtn)
            {
                nextTurn = true;
                nextTurnBtn = true;
            }
        }
        else if (nextTurnBtn)
        {
            nextTurnBtn = false;
        }
        if (nextTurn)
            {
                foreach (ShipScript ship in FindObjectsOfType<ShipScript>())
                {
                    ship.dist = ship.maxDist;
                }
                nextTurn = false;
            }
    }
}
