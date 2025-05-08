using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;
    public float airSpeed;
    public bool isHeavy;

    public float groundDrag;

    public float jumpForce;
    public float flyForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump = true;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Stats")]
    public float stamina;
    bool hasWater;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.1f, whatIsGround);

        MyInput();

        SpeedControl();

        //Handle drag
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;

        if (!isHeavy)
        {
            rb.useGravity = false;
        }

        else
        {
            rb.useGravity = true;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //jump when heavy
        if(Input.GetKeyDown(jumpKey) && readyToJump && grounded && isHeavy)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKey(jumpKey) && !isHeavy)
        {
            rb.AddForce(transform.up * flyForce, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.LeftShift) && !isHeavy && !grounded)
        {
            rb.AddForce(-transform.up * flyForce, ForceMode.Force);
        }

    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on ground
        rb.AddForce(moveDirection.normalized * moveSpeed * 50f, ForceMode.Force);

        //on Air
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        Vector3 verticalVel = new Vector3(0f, rb.linearVelocity.y, 0f);

        //Limit velocity
        if (flatVel.magnitude > moveSpeed && grounded)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }

        //Limit velocity
        if (flatVel.magnitude > airSpeed && !grounded)
        {
            Vector3 limitedVel = flatVel.normalized * airSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }

        //Limit vertical velocity when flying
        if (verticalVel.magnitude > airSpeed && !grounded && !isHeavy)
        {
            Vector3 limitedVel = verticalVel.normalized * airSpeed;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, limitedVel.y, rb.linearVelocity.z);
        }
    }

    private void Jump()
    {
        //reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    { 
        readyToJump = true;
    }

}
