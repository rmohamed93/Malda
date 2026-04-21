using UnityEngine;

public class EnemyFlying : MonoBehaviour
{
    public float moveSpeed = 2f;

    public Transform wallCheck;
    public float wallCheckDistance = 0.2f;

    private Rigidbody2D rb;
    private int direction = 1;
    private BoxCollider2D bc;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        bool wallAhead = Physics2D.Raycast(
            wallCheck.position,
            Vector2.right * direction,
            wallCheckDistance
        );

        if (wallAhead)
        {
            Flip();
        }
    }

    void Flip()
    {
        direction *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Stop()
    {
        rb.linearVelocity = new Vector2(0, 0);
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        bc.enabled = false;
    }
}
