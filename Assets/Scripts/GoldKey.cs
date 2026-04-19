using UnityEngine;

public class GoldKey : MonoBehaviour, IInteractable
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
        GameManager.Instance.hasGoldKey = true;
        player.SetHasGoldKey();

        // add id to gamemanager
        GameManager.Instance.collectedItems.Add(itemID);

        // remove item
        Destroy(gameObject);
    }
}
