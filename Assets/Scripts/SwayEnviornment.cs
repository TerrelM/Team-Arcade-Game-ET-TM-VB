using UnityEngine;

public class SwayEnviornment : MonoBehaviour
{
    public float speed;
    public float swayTiltX;
    public float swayTiltZ;
    private float swayAmount;
    private float swaySpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        swaySpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (swayAmount >= 1)
        {
            swaySpeed = -speed;
        }
        else if (swayAmount <= -1)
        {
            swaySpeed = speed;
        }

        swayAmount += swaySpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, swayAmount);

        Debug.Log("swayAmount: " + swayAmount);
    }
}
