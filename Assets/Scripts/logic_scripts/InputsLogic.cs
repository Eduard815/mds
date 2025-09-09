using UnityEngine;
using TMPro; //pentru texte
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class InputsLogic : MonoBehaviour
{
    public Mouse mouseScript;

    public GameObject UI_starinfo;
    private TMP_Text UI_metalamount;

    public int turn;
    private bool mouseDown = false;
    private GameObject selected = null;

    [SerializeField] private float doubleClickTime = 0.5f;
    private StarScript lastStarClicked;
    private float lastClickTime;
    void Start()
    {
        turn = 1;
        UI_metalamount = UI_starinfo.transform.Find("metalAmount").GetComponent<TMP_Text>();
    }

    void Update()
    {
        //verifica daca s-a apasat click dreapta
        if (Input.GetMouseButtonDown(1) && !mouseDown && selected != null)
        {
            mouseDown = true;
            var clickedObj = mouseScript.otherObject.gameObject;
            ShipScript ship = selected.GetComponent<ShipScript>();
            StarScript clickedStar = clickedObj.GetComponent<StarScript>();
            ShipScript clickedShip = clickedObj.GetComponent<ShipScript>();
            if (clickedStar != null && ship != null)
            {
                if (ship.currentStar.neighbours.Contains(clickedStar))
                {
                    ship.targetStar = clickedStar;
                    ship.move();
                }

            }
            else if (clickedShip != null && ship != null)
            {
                if (ship.health - clickedShip.power <= 0)
                {
                    ship.health = 0;
                }
                else
                {
                    ship.health -= clickedShip.power;
                    if (clickedShip.health - ship.power <= 0)
                    {
                        clickedShip.health = 0;
                    }
                    else
                    {
                        clickedShip.health -= ship.power;
                    }
                }
            }
        }
        else if (!Input.GetMouseButtonDown(1) && mouseDown) mouseDown = false;



        // verifica daca s-a apasat click stanga
        if (Input.GetMouseButtonDown(0) && !mouseDown)
        {
            mouseDown = true;
            //verifica pe ce tip d eobiect s-a dat click
            if (mouseScript.otherObject != null)
            {
                var clickedObj = mouseScript.otherObject.gameObject;
                selected = clickedObj;
                ShipScript ship = clickedObj.GetComponent<ShipScript>();
                StarScript star = clickedObj.GetComponent<StarScript>();
                if (ship != null)
                {
                    Debug.Log("You selected " + selected.name);
                    UI_starinfo.SetActive(false);
                }
                else if (star != null)
                {
                    UI_starinfo.SetActive(true);
                    UI_metalamount.text = star.metal.ToString();
                    Debug.Log("You selected " + selected.name);
                    Debug.Log("id " + star.id);
                    Debug.Log("neighbours " + star.neighbours[0]);

                    //calculates the time between clicks to determine whether there was a valid double click
                    float now = Time.time;
                    if (lastStarClicked == star && (now - lastClickTime) <= doubleClickTime)
                    {
                        SaveGalaxy.Instance.selectedStarId = star.id;
                        SaveGalaxy.Instance.selectedStarSeed = star.seed;
                        SaveGalaxy.Instance.selectedStarType = star.type;
                        SceneManager.LoadScene("SolarSystem");
                        return;
                    }

                    lastStarClicked = star;
                    lastClickTime = now;
                }
                    
                }
            else
            {
                selected = null;
                UI_starinfo.SetActive(false);
            }
            }
            else if (!Input.GetMouseButtonDown(0) && mouseDown) mouseDown = false;



    }
}