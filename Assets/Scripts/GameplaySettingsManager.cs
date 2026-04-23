using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplaySettingsManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void ReturnToHub()
    {
        SceneManager.LoadScene("LevelSelectHub");
    }
}
