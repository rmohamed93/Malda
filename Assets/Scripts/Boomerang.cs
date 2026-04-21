using UnityEngine;
using System.Collections;

public class Boomerang : Item
{
    public Transform firePoint;
    public GameObject boomerangPrefab;

    public float speed = 10f;
    public float spinSpeed = 1000f; // Degrees per second for that classic boomerang spin
    public float forwardDuration = 2f;
    public float pauseDuration = 1f;
    private Vector3 throwDirection;
    private Vector3 firePos;

    public override void Use()
    {
        // 1. Spawn the boomerang
        GameObject currentBoomerang = Instantiate(boomerangPrefab, firePoint.position, firePoint.rotation);

        // 2. Get the script and call the Throw method
        // Assuming 'transform.right' is the direction your player is facing
        currentBoomerang.GetComponent<Boomerang>().Throw(firePoint);

    }

    public void Throw(Transform fireP)
    {
        firePoint = fireP;

        if (firePoint.localPosition.x > 0)
        {
            throwDirection = Vector3.right;
        }
        else
        {
            throwDirection = Vector3.left;
        }
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
        if (firePoint != null)
        {
            Vector3 targetReturnPosition = firePoint.position;

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
