using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Player health
    public int maxHealth = 3;

    // Inventory
    public bool hasSword = false;
    public bool hasSilverKey = false;
    public bool hasGoldKey = false;

    // Collected world items
    public HashSet<string> collectedItems = new HashSet<string>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
