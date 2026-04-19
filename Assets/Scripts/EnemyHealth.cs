using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3;
    private SpriteRenderer spriteRenderer;
    private Color baseColor;
    private Animator anim;
    private EnemyPatrol enemyPatrol;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyPatrol = GetComponent<EnemyPatrol>();
        baseColor = spriteRenderer.color;
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("health: " + this.health);
        if (health <= 0)
        {
            Debug.Log("Enemy killed");
            enemyPatrol.Stop();
            anim.SetBool("Death", true);
            Destroy(gameObject, 3f);
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
