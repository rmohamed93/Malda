using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    public string sceneToLoad;
    public bool isLocked;
    public int levelStateSet;

    void Awake()
    {
        if (isLocked)
        {
            gameObject.GetComponent<Animator>().enabled = false;
        }
    }
    public void Interact(Player player)
    {
        if (isLocked)
        {
            Debug.Log("Door is locked");
            Unlock(player.GetHasSilverKey(), player.GetHasGoldKey());
        }
        else
        {
            // R.M. - Added this method to call on transition
            //ScreenTransition.Instance.LoadScene(sceneToLoad);
            GameManager.Instance.currentLevel = levelStateSet;
            SceneManager.LoadScene(sceneToLoad);

        }
    }

    private void Unlock(bool hasSilverKey, bool hasGoldKey)
    {
        if (hasSilverKey && hasGoldKey)
        {
            isLocked = false;
            Debug.Log("Door unlocked");
            gameObject.GetComponent<Animator>().enabled = true;
        }
        else
        {
            Debug.Log("Missing keys");
        }
    }

}
