using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3;
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("health: " + this.health);
        if (health <= 0)
        {
            Debug.Log("Enemy killed");
            Destroy(gameObject);
        }
    }
}
