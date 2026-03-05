using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectObjects : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Door1() {
        SceneManager.LoadScene("EnemyTestArea");
    }

    public void Door2() {
        SceneManager.LoadScene("EnemyTestArea");

    }

    public void Door3() {
        SceneManager.LoadScene("EnemyTestArea");

    }

    public void Door4() {
        SceneManager.LoadScene("EnemyTestArea");

    }

    public void Door5() {
        SceneManager.LoadScene("EnemyTestArea");
    }

    public void Door6() {
        SceneManager.LoadScene("EnemyTestArea");
    }

    public void saveNPC() {
        Debug.Log("Save");
    }
}
