using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PelletMovement : MonoBehaviour
{
    public PlayerController playerController;
    public float speed = 30.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //playerController = GameObject.Find("Player1").GetComponent<PlayerController>();
        //playerController = GameObject.Find("Player2").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //Debug.Log(playerController.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null && player != playerController)
        {
            Debug.Log(player.name);
        }
    }
}
