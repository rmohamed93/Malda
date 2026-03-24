using System.Numerics;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 5;
    public void TakeDamage(int damage, Vector2 damageSource)
    {
        if (isInvincible) return;
        health -= damage;
        Debug.Log("health: " + this.health);
        if (health <= 0)
        {
            Debug.Log("Player killed");
            Destroy(gameObject);
        }
    }
}
