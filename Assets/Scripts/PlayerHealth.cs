using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 5;
    private int currentHealth;

    [Header("Knockback")]
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.2f;

    [Header("Invincibility")]
    public float invincibilityTime = 1f;

    private bool isInvincible = false;
    private bool isKnockedBack = false;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public bool IsKnockedBack => isKnockedBack;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, Vector2 damageSource)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        Debug.Log("playerhealth:" + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(HandleKnockback(damageSource));
        StartCoroutine(HandleInvincibility());
    }

    public void Heal(int healAmount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += healAmount;
        }
        Debug.Log("playerhealth:" + currentHealth);

    }

    public void IncreaseHealth(int increaseAmount)
    {
        maxHealth += increaseAmount;
        Debug.Log("Player max health: " + maxHealth);
        Heal(increaseAmount);
    }

    IEnumerator HandleKnockback(Vector2 source)
    {
        isKnockedBack = true;

        Vector2 direction = ((Vector2)transform.position - source).normalized;

        direction.y = Mathf.Abs(direction.y) + 0.5f;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackDuration);

        isKnockedBack = false;
    }

    IEnumerator HandleInvincibility()
    {
        isInvincible = true;

        StartCoroutine(FlashEffect());

        yield return new WaitForSeconds(invincibilityTime);

        isInvincible = false;
    }

    IEnumerator FlashEffect()
    {
        float interval = 0.1f;

        while (isInvincible)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(interval);
        }

        spriteRenderer.enabled = true;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
