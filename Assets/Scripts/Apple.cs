using UnityEngine;

public class Apple : MonoBehaviour, IInteractable
{
    public int healAmount = 1;
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
            player.Heal(healAmount);
        }
        // remove item
        Destroy(gameObject);
    }
}
