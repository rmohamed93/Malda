using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    public string sceneToLoad;
    public AudioClip soundEffectClip;
    private AudioSource audioSource;
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
        SceneManager.LoadScene(sceneToLoad);
    }
}
