using System.Threading;
using UnityEngine;

public class bombBounce : MonoBehaviour
{
    private Rigidbody rb;
    public float bounceStrength;
    public float stretchScale;
    public float bounceTime;

    private float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Bomb bouncing.
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
            }

            float scaleBounceY = (bounceTime - timer) / bounceTime * (1 - stretchScale) + stretchScale;
            transform.localScale = Vector3.one * scaleBounceY;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //When touching Ground, Bounce.
        int objectLayer = collision.gameObject.layer;

        if (objectLayer == LayerMask.NameToLayer("Ground"))
        {
            timer = bounceTime;
            //Set bomb bounce force.
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, bounceStrength, rb.linearVelocity.z);
        }



        //If bomb hit player, explode.
        if (collision.gameObject.name == "Player1")
        {
            //Debug.Log("Hit player 1!");
            Explode();
        }

        if (collision.gameObject.name == "Player2")
        {
            //Debug.Log("Hit player 2!");
            Explode();
        }
    }

    private void Explode()
    {
        //Destroy(gameObject);
    }
}
