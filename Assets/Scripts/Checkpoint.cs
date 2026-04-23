using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform flagPosition;
    public AudioClip checkpointMusic;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnManager.Instance.SetRespawnPoint(flagPosition.position);
            Debug.Log("Checkpoint reached: " + flagPosition.position);

            if (checkpointMusic != null)
            {
                MusicManager.Instance.ChangeMusic(checkpointMusic);
            }
        }
    }
}
