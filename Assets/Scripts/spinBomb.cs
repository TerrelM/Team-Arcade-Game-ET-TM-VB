using UnityEngine;

public class spinBomb : MonoBehaviour
{
    public Vector3 spinSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(spinSpeed * Time.deltaTime);
    }
}
