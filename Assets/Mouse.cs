using UnityEngine;

public class Mouse : MonoBehaviour
{
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        // Get mouse position in world space
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // keep on the 2D plane

        // Move the area to the cursor
        transform.position = mousePos;
        //Debug.Log(mousePos);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Touched object: " + other.name);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"{other.name} exited the cursor area!");
    }
}
