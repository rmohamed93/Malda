using UnityEngine;
<<<<<<< Updated upstream

public class Interactable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerActions player = other.GetComponent<PlayerActions>();
            player?.SetCurrentInteractable(this);
        }
    }

=======
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.IO;

public class InteractableObject : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    public bool inRange = false;
    public UnityEvent onInteract;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(interactKey)) {
            onInteract.Invoke();
        }
    }

    // This fires automatically when something enters the trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is the Player
        if (other.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log("Player entered interaction range.");
        }
    }

    // This fires automatically when something leaves the trigger collider
>>>>>>> Stashed changes
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
<<<<<<< Updated upstream
            PlayerActions player = other.GetComponent<PlayerActions>();
            player?.ClearCurrentInteractable(this);
=======
            inRange = false;
            Debug.Log("Player left interaction range.");
>>>>>>> Stashed changes
        }
    }
}
