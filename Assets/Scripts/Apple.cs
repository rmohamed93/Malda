using UnityEngine;

public class Apple : MonoBehaviour, IInteractable
{
    public int healAmount = 1;
    public string itemID;

    void Awake()
    {
    }

    void Start()
    {
        if (GameManager.Instance.collectedItems.Contains(itemID))
        {
            Destroy(gameObject);
        }
    }
    public void Interact(Player player)
    {
        // heal player
        player.Heal(healAmount);

        // add id to gamemanager
        GameManager.Instance.collectedItems.Add(itemID);

        // remove item
        Destroy(gameObject);
    }
}
