using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScreenTransition : MonoBehaviour
{
	// R.M. - Created this for scene transitions.
	public static ScreenTransition Instance;
	public Animator animator; 

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

	public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeAndLoad(sceneName));
    }

    private IEnumerator FadeAndLoad(string sceneName)
    {
        animator.SetTrigger("FadeIn");

        // Wait for the animation to finish
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneName);
    }
}
