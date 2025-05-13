using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;
    public float airSpeed;
    public bool isHeavy;

    public float groundDrag;

    public float jumpTimer;
    public float jumpForce;
    public float maxJumpForce;
    public float minJumpForce;
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
    public float maxStamina;
    public float currentStamina;
    bool hasWater;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    public ItemManager ItemManager;
    public GameObject backPack;
    [SerializeField] public Image staminaBar;
    [SerializeField] public Image jumpBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        currentStamina = maxStamina;
        
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
            rb.mass = 1;
        }

        else
        {
            rb.mass = 5;
        }

        if(grounded && currentStamina < maxStamina)
        {
            currentStamina += Time.deltaTime * 20;
        }
        

        staminaBar.fillAmount = currentStamina / 100;
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
        if (Input.GetKey(jumpKey) && readyToJump && grounded && isHeavy && currentStamina > 30)
        {
            jumpTimer += Time.deltaTime;
            jumpBar.fillAmount = jumpTimer * 2;
        }
            
        if(Input.GetKeyUp(jumpKey) && readyToJump && grounded && isHeavy && currentStamina > 30)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if(Input.GetKey(jumpKey) && !grounded && isHeavy && rb.linearVelocity.y < 0 && currentStamina > 0)
        {
            rb.AddForce(transform.up * flyForce * 2, ForceMode.Force);
            currentStamina -= Time.deltaTime * 15;
        }

        if (Input.GetKey(jumpKey) && !isHeavy && currentStamina >= 0)
        {
            rb.AddForce(transform.up * flyForce, ForceMode.Force);       
        }

        if (Input.GetKey(KeyCode.LeftShift) && !isHeavy && !grounded)
        {
            rb.AddForce(-transform.up * flyForce, ForceMode.Force);
        }

        if (Input.GetKeyDown(KeyCode.Q) && ItemManager.hasItem == true)
        {
            DropItem();
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
        //Calculate the force for jumping
        float force = jumpForce * jumpTimer;
        if (force > maxJumpForce) force = maxJumpForce;
        if (force < minJumpForce) force = minJumpForce;

        //reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * force, ForceMode.Impulse);
        currentStamina -= force / 3;
        jumpTimer = 0f;
        jumpBar.fillAmount = 0f;
    }

    private void ResetJump()
    { 
        readyToJump = true;
    }

    private void ChangeGravity()
    {
        if (rb.useGravity)
        {
            rb.useGravity = false;
        }
        else
        {
            rb.useGravity = true;
        }
    }

    private void DropItem()
    {
        ItemManager.hasItem = false;
        ItemManager.hasRope = false;
        ItemManager.hasWater = false;
        isHeavy = false;
        foreach (Transform child in backPack.transform)
            child.gameObject.SetActive(false);
    }

}
