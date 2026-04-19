using UnityEngine;

public class TurretActions : MonoBehaviour
{
    public float timeBetweenShots = 3;
    public int Range = 10;

    public GameObject projectilePrefab;
    public Transform firePoint;

    public float shootForce = 20f;        // Bullet speed
    public float projectileLife = 3f;     // How long before the bullet disappears

    GameObject player;
    float cooldownTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, this.gameObject.transform.position);
        if (distance < Range) {
            float gameTime = Time.time;
            if (cooldownTime - gameTime < 0) {
                Shoot();
                cooldownTime = gameTime + timeBetweenShots;
            }
        }
    }

    void Shoot()
    {
        // Calculate the direction towards the player
        Vector3 directionToPlayer = (player.transform.position - firePoint.position).normalized;

        // Create the projectile
        GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        newProjectile.SetActive(true);

        // Apply force towards the player
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
        if (rb != null) {
            rb.AddForce(directionToPlayer * shootForce, ForceMode2D.Impulse);
        }

        // Clean up: Destroy THIS specific projectile after 'projectileLife' seconds.
        // We do this right here in the turret script!
        Destroy(newProjectile, projectileLife);
    }
}
