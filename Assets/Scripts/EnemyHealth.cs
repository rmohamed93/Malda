using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3;
    private SpriteRenderer spriteRenderer;
    private Color baseColor;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseColor = spriteRenderer.color;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("health: " + this.health);
        if (health <= 0)
        {
            Debug.Log("Enemy killed");
            Destroy(gameObject);
        }
        StartCoroutine(FlashRed());
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = baseColor;
    }
}
