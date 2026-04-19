using UnityEngine;

public class projectileScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy this projectile
        Destroy(gameObject);
    }
}
