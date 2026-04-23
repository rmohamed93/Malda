using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public AudioSource audioSource;
    public AudioClip defaultMusic;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (defaultMusic != null)
        {
            audioSource.clip = defaultMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void ChangeMusic(AudioClip newClip)
    {
        if (audioSource.clip == newClip) return;

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.loop = true;
        audioSource.Play();
    }
}