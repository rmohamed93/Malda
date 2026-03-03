using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float sprintMultiplier = 1.5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;
    private bool isSprinting;
    private bool jumpRequested;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        // Apply horizontal movement
        var currentSpeed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;
        rb.linearVelocity = new Vector2(moveInput.x * currentSpeed, rb.linearVelocity.y);

        // Apply jump (only once)
        if (jumpRequested && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        jumpRequested = false; // reset jump
    }
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        jumpRequested = value.isPressed;
    }

    public void OnSprint(InputValue value)
    {
        isSprinting = value.isPressed;
        Debug.Log("Sprint: " + isSprinting);
    }
}
