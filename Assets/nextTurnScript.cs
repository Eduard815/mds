using UnityEngine;

public class nextTurnScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    bool nextTurnBtn = false;
    bool nextTurn = false;

    [SerializeField] private Inventory inventory;
    [SerializeField] private RuntimeUI runtimeUI;
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
            nextTurn = false;
            foreach (ShipScript ship in FindObjectsOfType<ShipScript>())
            {
                ship.dist = ship.maxDist;
            }

            /// Updating the inventory by calling the ApplyTurn function right after ending the turn
            if (inventory != null){
                inventory.ApplyTurn();
            }

            /// Refreshing the UI to get the modified values from the inventory of the next turn
            if (runtimeUI != null){
                runtimeUI.RefreshUI();
            }
        }
    }
}
