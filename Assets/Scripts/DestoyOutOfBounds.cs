using UnityEngine;

public class DestoyOutOfBounds : MonoBehaviour
{
    public float boundDistance = 40;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > boundDistance)
        {
            Destroy(gameObject);
        }
    }
}
