using UnityEngine;

public class Mushroom : MonoBehaviour, IInteractable
{
    public int healthIncreaseAmount = 1;
    public string itemID;
    void Awake()
    {
    }

    public void Interact(Player player)
    {
        // increase max health
        player.IncreaseMaxHealth(healthIncreaseAmount);

        // add id to gamemanager
        GameManager.Instance.collectedItems.Add(itemID);

        // remove item
        Destroy(gameObject);
    }
}
