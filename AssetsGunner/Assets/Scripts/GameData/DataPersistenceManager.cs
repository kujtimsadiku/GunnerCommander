using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class newGame
{
	public static bool newgame = false;
}
public class DataPersistenceManager : MonoBehaviour
{
	[Header("File Storage Config")]
	[SerializeField] private string fileName;
	private GameData gameData;
	private FileDataHandler dataHandler;
	private List<IDataPersistence> dataPersistenceObj;
	public static DataPersistenceManager instance { get; private set; }

	private void Awake()
	{
		if (instance != null)
			Debug.LogError("There is a Data Persistance already");
		instance = this;
	}

	private void Start()
	{
		this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
		this.dataPersistenceObj = FindAllDataPersistenceObj();
		LoadGame();
	}

	public void NewGame()
	{
		newGame.newgame = true;
		this.gameData = new GameData();
	}

	public void LoadGame()
	{
		// Load any saved data from a file using the data handler
		this.gameData = dataHandler.Load();
		// if no data can be loaded, initialize to a new game
		if (this.gameData == null)
		{
			Debug.Log("No data was found.");
			NewGame();
		}
		foreach (IDataPersistence dataPersistenceObjects in dataPersistenceObj)
		{
			dataPersistenceObjects.LoadData(gameData);
		}
	}

	public void SaveGame()
	{
		// pass the data to other scripts so they can update it
		foreach (IDataPersistence dataPersistenceObjects in dataPersistenceObj)
		{
			dataPersistenceObjects.SaveData(ref gameData);
		}
		// save that data to a file using the data handler
		dataHandler.Save(gameData);
	}

	private void OnApplicationQuit()
	{
		SaveGame();
	}

	private List<IDataPersistence> FindAllDataPersistenceObj()
	{
		IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
		return new List<IDataPersistence>(dataPersistenceObjects);
	}
}

