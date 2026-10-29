using UnityEngine;
using UnityEngine.UIElements;

public class SwayEnviornment : MonoBehaviour
{
    [Header("Sway Acceleration")]
    [Tooltip("How fast we approach the max sway speed.")]
    public float acceleration;
    [Header("Sway Maximum Speed")]
    [Tooltip("The max speed the level can sway.")]
    public float maxSpeed;
    [Header("Sway Tilt Amounts")]
    [Tooltip("How far to tilt in the x and z axis.")]
    public float swayTiltX;
    public float swayTiltZ;

    //Private Veriables
    private Rigidbody rb;
    private Vector3 position;
    private float swayAmount;
    private float swayAcceleration;
    private float swayVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        position = transform.position;
        swayAcceleration = acceleration;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Set the velocity of sway. (For a smoother more naturally looking motion.)
        swayVelocity += swayAcceleration * Time.fixedDeltaTime;
        if (swayVelocity > maxSpeed)
        {
            swayVelocity = maxSpeed;
        }
        if (swayVelocity < -maxSpeed)
        {
            swayVelocity = -maxSpeed;
        }

        //If we are swaying too much into one side we turn around.
        if (swayAmount >= 1)
        {
            swayAcceleration = -acceleration;
        }
        else if (swayAmount <= -1)
        {
            swayAcceleration = acceleration;
        }

        //Add velocity and calculate the tilt amounts.
        swayAmount += swayVelocity * Time.fixedDeltaTime;
        float tiltAmountX = swayAmount * swayTiltX;
        float tiltAmountZ = swayAmount * swayTiltZ;



        //Apply Rotations
        rb.position = position;
        rb.rotation = Quaternion.Euler(tiltAmountX, 0, tiltAmountZ);
    }
}
