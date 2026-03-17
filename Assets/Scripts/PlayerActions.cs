using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float sprintMultiplier = 1.5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;
    private bool isSprinting;
    private bool jumpRequested;
    private bool escapePressed;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public GameObject settingsPanel;

    private IInteractable currentInteractable;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        if (escapePressed)
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
            Debug.Log("Settings panel is active: " + settingsPanel.activeSelf);
        }
        escapePressed = false;

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
    public void OnOpenMenu(InputValue value)
    {
        escapePressed = value.isPressed;
        Debug.Log(escapePressed);
    }

    public void OnSprint(InputValue value)
    {
        isSprinting = value.isPressed;
    }

    public void OnInteract(InputValue value)
    {
        currentInteractable?.Interact();
    }

    public void SetCurrentInteractable(Interactable interactable)
    {
        currentInteractable = interactable.GetComponent<IInteractable>();
    }

    public void ClearCurrentInteractable(Interactable interactable)
    {
        if (currentInteractable == interactable.GetComponent<IInteractable>())
        {
            currentInteractable = null;
        }
    }
}
