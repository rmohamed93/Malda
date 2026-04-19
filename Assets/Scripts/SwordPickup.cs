using UnityEngine;

public class SwordPickup : MonoBehaviour, IInteractable
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
        // set sword bool
        GameManager.Instance.hasSword = true;
        player.SetHasSword();

        // add id to gamemanager
        GameManager.Instance.collectedItems.Add(itemID);

        // remove item
        Destroy(gameObject);
    }
}
