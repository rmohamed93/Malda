using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    public string sceneToLoad;
    public AudioClip soundEffectClip;
    public bool isLocked;
    private AudioSource audioSource;
    private PlayerActions player;

    void Awake()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<PlayerActions>();
    }
    public void Interact()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found");
        }
        if (soundEffectClip == null)
        {
            Debug.LogError("AudioClip not assigned");
        }
        if (audioSource != null && soundEffectClip != null)
        {
            audioSource.PlayOneShot(soundEffectClip);
        }

        if (isLocked)
        {
            Debug.Log("Door is locked");
            Unlock();
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void Unlock()
    {
        if (player.GetHasKey())
        {
            this.isLocked = false;
            Debug.Log("Door has been unlocked");
        }
    }

}
