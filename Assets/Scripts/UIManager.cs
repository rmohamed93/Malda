using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class ButtonManager : MonoBehaviour
{
	public GameObject settingsPanel;

    public void StartGame()
    {
        Debug.Log("New Game Button Selected");
        SceneManager.LoadScene("LevelSelectHub");
    }
	
	public void SaveGame() {
		Debug.Log("Save Game has been selected");
		
		PlayerData data = new PlayerData();
		
		data.items = new string[1];
		data.items[0]="Test";
		data.levelsCompleted = new int[1];
		data.levelsCompleted[0] = -1;
		//get items and levels that have been completed
		
		string json = JsonUtility.ToJson(data);
		string path = Application.persistentDataPath + "/MaldaSave.json";
		File.WriteAllText(path, json);
		Debug.Log(path + "/MaldaSave.json" + " saved");
	}
	
	public void LoadGame()
	{
		Debug.Log("Load Game Button Selected");
		
		PlayerData data;
		
		string path = Application.persistentDataPath + "/MaldaSave.json";
		
		if (File.Exists(path)) {
			string json = File.ReadAllText(path);
			data = JsonUtility.FromJson<PlayerData>(json);
			
			//Set the game to have the items and levels completed from the save
		}
		else{
			Debug.Log("No save file exists!");
			return;
		}

		SceneManager.LoadScene("LevelSelectHub");
	}

	public void openSettings()
	{
		settingsPanel.SetActive(true);
	}

	public void saveSettings()
	{
		settingsPanel.SetActive(false);
	}

    public void EndGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
	
	
}

[System.Serializable]
public class PlayerData {
	public string[] items;
	public int[] levelsCompleted;
}


