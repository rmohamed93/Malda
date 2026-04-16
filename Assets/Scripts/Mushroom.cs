using UnityEngine;

public class Mushroom : MonoBehaviour, IInteractable
{
    public int healthIncreaseAmount = 1;
    private PlayerHealth player;
    void Awake()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<PlayerHealth>();
    }

    public void Interact()
    {
        if (player != null)
        {
            player.IncreaseHealth(healthIncreaseAmount);
        }
        // remove item
        Destroy(gameObject);
    }
}
