using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float sprintMultiplier = 1.5f;
    public int health = 100;
    public AudioClip jumpClip;
    public Item swordItem;


    private Rigidbody2D rb;
    private AudioSource audioSource;
    private Vector2 moveInput;
    private bool isGrounded;
    private bool wasGrounded;
    private bool isSprinting;
    private bool jumpRequested;
    private bool escapePressed;
    private int jumpCount;
    private bool useSwordPressed;
    private bool isBusy;
    private Item currentItem;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public GameObject settingsPanel;

    private IInteractable currentInteractable;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found");
        }
        if (jumpClip == null)
        {
            Debug.LogError("AudioClip not assigned");
        }
        swordItem?.Initialize(gameObject);
    }

    void FixedUpdate()
    {

        // Menu open/close
        if (escapePressed)
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
        escapePressed = false;

        // Ground check
        bool currentlyGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        // Landing detection
        if (currentlyGrounded && !wasGrounded)
        {
            jumpCount = 0;
        }

        isGrounded = currentlyGrounded;
        wasGrounded = currentlyGrounded;

        // Apply horizontal movement
        var currentSpeed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;
        rb.linearVelocity = new Vector2(moveInput.x * currentSpeed, rb.linearVelocity.y);

        // Apply jump
        if (jumpRequested && jumpCount < 2)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
            PlayJumpAudio();
        }
        jumpRequested = false; // reset jump

        if (useSwordPressed)
        {
            Debug.Log("sword swung");
        }
        useSwordPressed = false;
    }
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        jumpRequested = value.isPressed;
    }
    public void PlayJumpAudio()
    {
        if (audioSource != null && jumpClip != null)
        {
            audioSource.PlayOneShot(jumpClip);
        }
    }
    public void OnOpenMenu(InputValue value)
    {
        escapePressed = value.isPressed;
    }

    public void OnSprint(InputValue value)
    {
        isSprinting = value.isPressed;
    }

    public void OnInteract(InputValue value)
    {
        currentInteractable?.Interact();
    }

    public void OnUseSword()
    {
        UseItem(swordItem);
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

    void UseItem(Item item)
    {
        if (item == null || isBusy)
        {
            return;
        }
        currentItem = item;
        isBusy = true;

        item.Use();

        Invoke(nameof(ClearBusy), 0.3f);
    }

    void ClearBusy()
    {
        isBusy = false;
    }
}
