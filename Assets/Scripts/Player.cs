using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement values")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float sprintMultiplier = 1.5f;
    public float groundCheckRadius = 0.2f;
    private int jumpCount;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float riseGravityMultiplier = 1.2f;

    [Header("Input Bools")]
    private bool isGrounded;
    private bool wasGrounded;
    private bool isSprinting;
    private bool jumpRequested;
    private bool escapePressed;
    private bool isBusy;
    private bool jumpHeld;
    private bool facingRight = true;
    private bool hasSword;
    private bool hasSilverKey;
    private bool hasGoldKey;

    [Header("Components")]
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveInput;
    private Item currentItem;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject settingsPanel;
    private IInteractable currentInteractable;
    public AudioClip jumpClip;
    public Item swordItem;
    public Item boomerangItem;
    public PlayerHealth playerHealth;
    private Transform attackPoint;
    private Animator anim;

    void Awake()
    {
        // get rigidbody of player
        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        attackPoint = transform.Find("AttackPoint");

        // get player health component
        playerHealth = GetComponent<PlayerHealth>();

        anim = GetComponent<Animator>();

        // get and initialize the sword on player
        swordItem?.Initialize(gameObject);

        boomerangItem?.Initialize(gameObject);
    }

    void Start()
    {
        hasSword = GameManager.Instance.hasSword;
        hasSilverKey = GameManager.Instance.hasSilverKey;
        hasGoldKey = GameManager.Instance.hasGoldKey;
    }

    void FixedUpdate()
    {
        // Menu open/close
        if (escapePressed)
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
        escapePressed = false;

        // knockback detection
        if (playerHealth.IsKnockedBack) return;

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

        anim.SetBool("Ground", isGrounded);

        // Apply horizontal movement
        var currentSpeed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;
        rb.linearVelocity = new Vector2(moveInput.x * currentSpeed, rb.linearVelocity.y);
        anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        anim.SetFloat("Jump", Mathf.Abs(rb.linearVelocity.y));

        // Flip
        HandleFlip();

        // Apply jump
        if (jumpRequested && jumpCount < 2)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
        }
        jumpRequested = false; // reset jump

        // gravity alterations
        if (rb.linearVelocity.y < 0)
        {
            // Falling
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !jumpHeld)
        {
            // Short hop
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0 && jumpHeld)
        {
            // slightly stronger gravity while rising
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (riseGravityMultiplier - 1) * Time.fixedDeltaTime;
        }


    }


    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            jumpRequested = true;
        }
        jumpHeld = value.isPressed;
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
        currentInteractable?.Interact(this);
    }

    public void OnUseSword()
    {
        UseItem(swordItem);
    }

    public void OnUseBoomerang()
    {
        UseItem(boomerangItem);
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

        if (currentItem is Sword)
        {
            Debug.Log("Sword Attack");
            anim.SetBool("Attacking", true);
        }

        if (currentItem is Boomerang)
        {
            Debug.Log("Boomerang Throw");
        }

        Debug.Log("Using Item");

        Invoke(nameof(ClearBusy), 0.3f);
    }
    void ClearBusy()
    {
        isBusy = false;
        anim.SetBool("Attacking", false);
    }

    void HandleFlip()
    {
        if (moveInput.x > 0.1f && !facingRight)
        {
            Flip();
        }
        else if (moveInput.x < -0.1f && facingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        facingRight = !facingRight;

        // Flip sprite
        spriteRenderer.flipX = !spriteRenderer.flipX;

        // Flip attackpoint 
        Vector3 currPos = attackPoint.localPosition;
        currPos *= -1;
        attackPoint.localPosition = currPos;
    }

    public void Heal(int amount)
    {
        playerHealth.Heal(amount);
    }
    public void IncreaseMaxHealth(int amount)
    {
        playerHealth.IncreaseMaxHealth(amount);
    }

    public void SetHasSword()
    {
        hasSword = true;
    }
    public void SetHasSilverKey()
    {
        hasSilverKey = true;
    }
    public void SetHasGoldKey()
    {
        hasGoldKey = true;
    }
}
