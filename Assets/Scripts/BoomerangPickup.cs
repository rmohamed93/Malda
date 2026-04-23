using UnityEngine;

public class BoomerangPickup : MonoBehaviour, IInteractable
{

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
        // set boomerang bool
        GameManager.Instance.hasBoomerang = true;
        player.SetHasBoomerang();

        // add id to gamemanager
        GameManager.Instance.collectedItems.Add(itemID);

        // remove item
        Destroy(gameObject);
    }
}