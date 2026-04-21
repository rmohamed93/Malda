using UnityEngine;
using System.Collections;

public class boomerang : MonoBehaviour, IInteractable
{

    public Transform firePoint;

    public float speed = 10f;
    public float spinSpeed = 1000f; // Degrees per second for that classic boomerang spin
    public float forwardDuration = 2f;
    public float pauseDuration = 1f;

    GameObject player;

    private Transform playerTransform;
    private Vector3 throwDirection;

    public void Interact(Player player)
    {
        // 1. Spawn the boomerang
        GameObject currentBoomerang = Instantiate(this, firePoint.position, Quaternion.identity);
        currentBoomerang.SetActive(true);
        
        // 2. Get the script and call the Throw method
        // Assuming 'transform.right' is the direction your player is facing
        currentBoomerang.GetComponent<boomerang>().Throw(transform, transform.right); 
    }

    public void Throw(Transform thrower, Vector3 direction)
    {
        playerTransform = thrower;
        throwDirection = direction.normalized;
        
        // Start the behavior sequence
        StartCoroutine(BoomerangRoutine());
    }

    void Update()
    {
        // Visually spin the boomerang on the Z-axis (standard for 2D)
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
    }

    private IEnumerator BoomerangRoutine()
    {
        // --- PHASE 1: Fly Forward ---
        float timer = 0f;
        while (timer < forwardDuration)
        {
            transform.position += throwDirection * speed * Time.deltaTime;
            timer += Time.deltaTime;
            
            // Wait until the next frame before continuing the loop
            yield return null; 
        }

        // --- PHASE 2: Pause ---
        yield return new WaitForSeconds(pauseDuration);

        // --- PHASE 3: Return ---
        // You specified returning to the player's position *at that moment in time*.
        // We capture that exact position here so it doesn't home in if the player keeps moving.
        if (playerTransform != null)
        {
            Vector3 targetReturnPosition = playerTransform.position;

            // Keep moving until the boomerang is very close to that captured point
            while (Vector3.Distance(transform.position, targetReturnPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetReturnPosition, speed * Time.deltaTime);
                yield return null;
            }
        }

        // --- PHASE 4: Cleanup ---
        // Destroy the boomerang once it reaches the target (or add it back to player inventory)
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy this projectile
        Destroy(gameObject);
    }
}
