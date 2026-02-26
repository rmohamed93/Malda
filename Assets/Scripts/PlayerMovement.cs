using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private bool isGrounded;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    void Start()
    {
	    rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
	float moveInput = Input.GetAxis("Horizontal");
	rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

	if (Input.GetButtonDown("Jump") && isGrounded) 
	{
		rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
	}
    }

    void FixedUpdate() 
    {
	isGrounded = Physics2D.OverlapCircle(
			groundCheck.position,
			groundCheckRadius,
			groundLayer
			);
    }
}
