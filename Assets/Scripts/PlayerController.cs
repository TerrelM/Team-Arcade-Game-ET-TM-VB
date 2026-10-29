using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private CharacterController cc;
    private Rigidbody rb;
    private CapsuleCollider capsule;
    private InputSystem_Actions inputActions;

    //========================================================================================== >> INPUT VARIABLES <<
    //Move
    private Vector2 move;
    //Jump
    private bool jump;
    private bool jumpPressed;
    private bool jumpReleased;
    private float jumpBufferTimer;
    //Attack
    private bool attack;
    private bool attackPressed;
    private bool attackReleased;

    //=================================================================================================== >> FORCES <<
    //Velocity
    //[Header("Velocity")]
    //[Tooltip("The player's velocity.")]
    //public Vector3 velocity;

    //Walk Forces
    [Header("Walk Forces")]
    [Tooltip("The player's walk speed.")]
    public float walkSpeed;
    [Tooltip("How fast it takes a player to reach top speed.")]
    public float walkAcceleration;
    [Tooltip("How fast it takes for a player stop.")]
    public float walkDeceleration;
    [Tooltip("How fast it takes for a player to turn.")]
    public float turnAcceleration;
    
    //Jump Forces
    [Header("Jump Forces")]
    [Tooltip("The force of a player's jump.")]
    public float jumpForce;
    [Tooltip("How much a player's jump is reduced when releasing jump.")]
    public float jumpReduction;

    //Gravity Forces
    [Header("Gravity Forces")]
    private float gravity;
    [Tooltip("How much gravity is applied when falling.")]
    public float gravityStrong;
    [Tooltip("How much gravity is applied when jumping.")]
    public float gravityWeak;

    //Timers & Messurements
    [Header("Timers & Measurements")]
    [Tooltip("Timer for how long a jump buffer with registar before it is invalid.")]
    public float jumpBuffer;
    //Prevents someone from reducing their jump multiple times.
    private bool startDecent;
    [Tooltip("How far to check for ground.")]
    public float groundTolerance = 0.1f;
    


    private void OnEnable()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }





    void Start()
    {
        //cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        gravity = gravityStrong;
        startDecent = false;
    }

    void Update()
    {
        //============================================================================================ >> INPUT ACTIONS <<
        //Move Action
        move = inputActions.Player.Move.ReadValue<Vector2>();

        //Jump Action
        jump = inputActions.Player.Jump.IsPressed();
        if (inputActions.Player.Jump.WasPressedThisFrame())
        {
            jumpPressed = true;
        }
        if (inputActions.Player.Jump.WasReleasedThisFrame())
        {
            jumpReleased = true;
        }

        //Attack Action
        attack = inputActions.Player.Attack.IsPressed();
        if (inputActions.Player.Attack.WasPressedThisFrame())
        {
            attackPressed = true;
        }
        if (inputActions.Player.Attack.WasReleasedThisFrame())
        {
            attackReleased = true;
        }
    }
    void FixedUpdate()
    {
        //Start Jump Buffer
        //Debug.Log("Jump Pressed: " + jumpBufferTimer);
        if (jumpPressed && (jumpBufferTimer <= 0))
        {
            jumpBufferTimer = jumpBuffer;
        }

        //Jump Buffer Count Down
        if (jumpBufferTimer > 0)
        {
            jumpBufferTimer -= Time.fixedDeltaTime;
        }


        
        //================================================================================================== >> WALKING <<

        float acceleration = 0;

        if (move.x != 0)
        {
            if ((move.x * rb.linearVelocity.x) > 0)
            {
                //If moving forward, use regular acceleration.
                acceleration = walkAcceleration;
            }
            else
            {
                if (IsGrounded())
                {
                    //If changing direction on Ground, use turn acceleration.
                    acceleration = turnAcceleration;
                }
                else
                {
                    //If changing direction off Ground, use regular acceleration.
                    acceleration = walkAcceleration;
                }
            }
        }
        else
        {
            if (IsGrounded())
            {
                //If not moving on Ground, use deceleration;
                acceleration = walkDeceleration;
            }
            else
            {
                //If not moving off Ground, use no deceleration;
                acceleration = 0;
            }
        }

        rb.linearVelocity = new Vector3(Mathf.MoveTowards(rb.linearVelocity.x, walkSpeed * move.x, acceleration * Time.fixedDeltaTime), rb.linearVelocity.y, rb.linearVelocity.z);

        //======================================================================================== >> JUMPING & FALLING <<
        //When falling, apply Gravity.
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y - gravity * Time.fixedDeltaTime, rb.linearVelocity.z);

        if (IsGrounded() == false)
        {
            
        }
        else
        {
            //When on Ground, reset vertical variables.
            gravity = gravityStrong;

            if (rb.linearVelocity.y < 0)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            }

            //When pressing Jump Buffer on Ground, jump.
            if (jumpBufferTimer > 0)
            {
                startDecent = true;
                jumpBufferTimer = 0;
                gravity = gravityWeak;
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            }
        }


        //When Releasing Jump, fall faster.
        if (startDecent && jumpReleased)
        {
            startDecent = false;
            gravity = gravityStrong;

            if (rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * jumpReduction, rb.linearVelocity.z);
            }
        }

        //Reset Inputs
        jumpPressed = false;
        jumpReleased = false;
        attackPressed = false;
        attackReleased = false;
    }

    

    private bool IsGrounded()
    {
        //Raycast with direction... and… MAGNITUDE! OH YEAH!
        RaycastHit hit;
        Vector3 origin = transform.position + capsule.center + Vector3.down * (capsule.height / 2f);
        float magnitude = groundTolerance;

        //Result
        bool result = Physics.Raycast(origin, Vector3.down, out hit, magnitude);

        //Raycast Debug
        //Debug.Log("Grounded: " + result);
        //Debug.DrawRay(origin, Vector3.down * magnitude, Color.green);

        return result;
    }
}
