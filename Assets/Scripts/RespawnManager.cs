using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;
    public Vector3 respawnPoint;
    private Vector3 currPoint;
    private int currlvl = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            currlvl = GameManager.Instance.currentLevel;
            currPoint = respawnPoint;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (currlvl != GameManager.Instance.currentLevel)
        {
            currlvl = GameManager.Instance.currentLevel;
            ResetSpawnPoint();
        }
    }

    public void SetRespawnPoint(Vector3 newPoint)
    {
        currPoint = newPoint;
    }

    public void ResetSpawnPoint()
    {
        currPoint = respawnPoint;
    }

    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = respawnPoint;
    }
}
