using UnityEngine;

public class SilverKey : MonoBehaviour, IInteractable
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
    public float rotationSpeed = 200f;
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
    public void Interact(Player player)
    {
        // set sword bool
        GameManager.Instance.hasSilverKey = true;
        player.SetHasSilverKey();

        // add id to gamemanager
        GameManager.Instance.collectedItems.Add(itemID);

        // remove item
        Destroy(gameObject);
    }
}
