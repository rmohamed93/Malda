using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public GameObject player;
    public Transform respawnPoint;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            player.transform.position = respawnPoint.position;
        }
    }

}
