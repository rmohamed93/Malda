using UnityEngine;

public class Sword : Item
{
    public GameObject hitboxPrefab;
    public Transform attackPoint;
    public float attackDuration = 0.2f;

    public override void Use()
    {
        GameObject hitbox = Instantiate(
            hitboxPrefab,
            attackPoint.position,
            attackPoint.rotation
        );
        Destroy(hitbox, attackDuration);
    }
}
