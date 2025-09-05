using UnityEngine;
using TMPro; //pentru texte

public class InputsLogic : MonoBehaviour
{
    public Mouse mouseScript;

    public GameObject UI_starinfo;
    private TMP_Text UI_metalamount;

    private bool mouseDown = false;
    private GameObject selected = null;
    void Start()
    {
        UI_metalamount = UI_starinfo.transform.Find("metalAmount").GetComponent<TMP_Text>();
    }

    void Update()
    {
        //verifica daca s-a apasat click dreapta
        if (Input.GetMouseButtonDown(1) && !mouseDown)
        {
            mouseDown = true;
            var clickedObj = mouseScript.otherObject.gameObject;
            ShipScript ship = selected.GetComponent<ShipScript>();
            StarScript star = clickedObj.GetComponent<StarScript>();
            if (star != null && ship != null)
            {
                if (ship.currentStar.neighbours.Contains(star))
                {
                    ship.currentStar = star;
                    ship.move();
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