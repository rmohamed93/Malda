using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("New Game Button Selected");
        SceneManager.LoadScene("LevelSelectHub");
    }

    public void endGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
