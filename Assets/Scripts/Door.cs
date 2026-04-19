using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    public string sceneToLoad;
    public bool isLocked;

    void Awake()
    {
    }
    public void Interact(Player player)
    {
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
        // wait
    }

}
