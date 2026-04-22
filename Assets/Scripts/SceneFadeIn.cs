using UnityEngine;

// R.M. - More transition code.
public class SceneFadeIn : MonoBehaviour
{
    void Start()
    {
        if (ScreenTransition.Instance != null)
        {
            ScreenTransition.Instance.animator.SetTrigger("FadeIn");
        }
    }
}