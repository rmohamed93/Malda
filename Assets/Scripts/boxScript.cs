using UnityEngine;

public class boxScript : MonoBehaviour
{

    public int health = 2;
    public Sprite damagedSprite;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the SpriteRenderer component on this object
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Projectile") {
            recieveDamage();
        }

    }

    private void recieveDamage() {
        health = health - 1;
        spriteRenderer.sprite = damagedSprite;
        if (health == 0) {
            Destroy(this.gameObject);
        }
    }
}
