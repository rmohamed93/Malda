using UnityEngine;

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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerActions player = other.GetComponent<PlayerActions>();
            player?.ClearCurrentInteractable(this);
        }
    }
}
