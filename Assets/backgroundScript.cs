using UnityEngine;

public class backgroundScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float constantZ;
    void Start()
    {
        constantZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = transform.parent.position * -0.1f;
        transform.position = new Vector3(transform.position.x,transform.position.y,constantZ);
        //Debug.Log(transform.position);
    }
}
