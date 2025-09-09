using UnityEngine;

public class cameraScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float zoomSpeed = 5f;
    public float minZoom = 2f;
    public float maxZoom = 10f;
    public float areaSize = 80f;

    private Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    public float speed = 5f;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            cam.orthographicSize -= scroll * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }

        // Get input
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down arrows

        // Create movement vector
        Vector2 movement = new Vector2(horizontal, vertical) * speed * Time.deltaTime;

        // Apply movement
        transform.Translate(movement, Space.World);


        // Limiting camera movement to the map's borders
        float verticalLimit = cam.orthographicSize;
        float horizontalLimit = verticalLimit * cam.aspect;

        float minX = -areaSize / 2 + horizontalLimit;
        float maxX = areaSize / 2 - horizontalLimit;
        float minY = -areaSize / 2 + verticalLimit + 1;
        float maxY = areaSize / 2 - verticalLimit;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}
