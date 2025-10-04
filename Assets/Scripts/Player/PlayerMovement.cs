using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed;
    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool canJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check Settings")]
    public float playerHeight;
    public LayerMask groundLayer;
    public float groundDistance = 0.2f;
    public float coyoteTime = 0.1f; // optional small grace window
    public bool grounded;
    private bool wasGroundedLastFrame;
    private bool hasLeftGround;
    private float timeSinceGrounded;

    [Header("Variable Jump Settings")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = .5f;
    private float jumpTimeHeld;
    public float minJumpTime = 0.1f;
    private bool isJumping;

    public Transform orientation;
    public Transform groundCheck;

    float horizontalInput;
    float verticalInput;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        grounded = true;
        ResetJump();
    }

    public void Update()
    {
        //grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if (grounded)
            timeSinceGrounded = 0f;
        else
            timeSinceGrounded += Time.deltaTime;

        // === Reliable jump reset ===
        if (grounded && rb.linearVelocity.y <= 0.1f) ResetJump();

        PlayerInput();
        SpeedControl();

        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;

        HandleVariableJump();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(jumpKey) && canJump && (grounded || timeSinceGrounded < coyoteTime))
        {
            canJump = false;
            Jump();
        }
    }


    private void MovePlayer()
    {
        Vector3 moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // Grounded
        if(grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }

        // Air
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        isJumping = true;
        jumpTimeHeld = 0f;

    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }

    private void HandleVariableJump()
    {
        if (isJumping)
            jumpTimeHeld += Time.deltaTime;

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            isJumping = false;
        }
        else if (rb.linearVelocity.y > 0)
        {
            if (!Input.GetKey(jumpKey))
            {
                rb.linearVelocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }

    private IEnumerator JumpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canJump = true;
    }


    private void ResetJump()
    {
        canJump = true;
    }

}
